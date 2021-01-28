using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public LayerMask monster;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, monster))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else 
        {
            Cursor.SetCursor(normalTexture, hotSpot, cursorMode);

        }
    }
}
