using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshRenderer hoverShader;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO have this tile determine when the mouse is not longer pressed down over it
        // When this happens, the shader should turn off
        
        
    }

    private void OnMouseDown()
    {
        hoverShader.enabled = true;
        Debug.Log("Enabled Hover Shader");
    }

    private void OnMouseUp()
    {
        hoverShader.enabled = false;
        Debug.Log("Disabled Hover Shader");
    }
}
