using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public MeshRenderer hoverShader;

    public Material mat;


    private float intensity = 1.0f;
    private float intensityDelta = 0.01f;
    private float minIntensity = 0.0f;
    private float maxIntensity = 1.0f;

    private float minX;
    private float maxX;
    private float minZ;
    private float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("Vector1_1E953448", intensity);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hoverShader.enabled)
        {
            intensity = Mathf.Clamp((intensity - intensityDelta), minIntensity, maxIntensity);
            mat.SetFloat("Vector1_1E953448", intensity);

            if (intensity == 0.0f)
            {
                hoverShader.enabled = false;
            }
        }
        
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }
        hoverShader.enabled = true;
        intensity = maxIntensity;
        Debug.Log("Enabled Hover Shader");
    }

    /*private void OnMouseUp()
    {
        hoverShader.enabled = false;
        Debug.Log("Disabled Hover Shader");
    }*/
}
