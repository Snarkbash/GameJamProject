using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private float minZoom = 15.0f;
    private float maxZoom = 70.0f;
    private float zoomSens = 50.0f;
    private void Update()
    {
        Vector3 desiredPos = target.position + offset;

        transform.position = desiredPos;

        float fov = Camera.main.fieldOfView;

        fov -= Input.GetAxis("Mouse ScrollWheel") * zoomSens;
        fov = Mathf.Clamp(fov, minZoom, maxZoom);
        Camera.main.fieldOfView = fov;

    }
}
