using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using TMPro;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Vector3 directionOfLine(float angle)
    {
        angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public List<Transform> sightedTargets;

    [Header("Visual")]
    public float meshResolution;
    public bool showFragmentLines;

    public struct ViewCastInfo
    {
        public bool isHit;
        public Vector3 endPoint;
        public float dst;
        public float angle;

        public ViewCastInfo (bool _isHit, Vector3 _endPoint, float _dst, float _angle)
        {
            isHit = _isHit;
            endPoint = _endPoint;
            dst = _dst;
            angle = _angle;
        }
    }

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private void Update()
    {
        FindVisibleTargets();

        DrawFieldOfView();
    }

    void FindVisibleTargets()
    {
        sightedTargets.Clear();

        Collider[] targetsInSight = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInSight.Length; i++)
        {
            Transform possibleTarget = targetsInSight[i].transform;
            Vector3 dirToTarget = (possibleTarget.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, dirToTarget) <= viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, possibleTarget.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    sightedTargets.Add(possibleTarget);
                }
            }
        }
    }

    ViewCastInfo ViewCast(float localAngle)
    {
        Vector3 dir = directionOfLine(localAngle);

        RaycastHit hit;

        if(Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, localAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, localAngle);
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        for(int i = 0; i <= stepCount; i++)
        {
            float lineAngle = -viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(lineAngle);
            viewPoints.Add(newViewCast.endPoint);

            if (showFragmentLines)
                Debug.DrawLine(transform.position, transform.position + directionOfLine(lineAngle) * viewRadius, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] trianglePoints = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                trianglePoints[i * 3] = 0;
                trianglePoints[i * 3 + 1] = i + 1;
                trianglePoints[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = trianglePoints;
        viewMesh.RecalculateNormals();
    }
}


[CustomEditor (typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fieldOfView = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.viewRadius);

        Vector3 lineA = fieldOfView.directionOfLine(-fieldOfView.viewAngle / 2);
        Vector3 lineB = fieldOfView.directionOfLine(fieldOfView.viewAngle / 2);

        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + lineA * fieldOfView.viewRadius);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + lineB * fieldOfView.viewRadius);
    }
}
