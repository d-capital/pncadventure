using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public Texture2D cursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseOver()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}