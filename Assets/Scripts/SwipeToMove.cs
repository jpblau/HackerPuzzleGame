using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToMove : MonoBehaviour
{
    private const float mouseDist = 2f;
    private const int button = 0;

    public Camera InputCamera;
    [Range(1f, 5f)]
    public float Sensitivity = 5f;

    /// <summary>
    /// Is the mouse currently down?
    /// </summary>
    private bool mouseDown;

    /// <summary>
    /// The mouse's position when it was last pressed down
    /// </summary>
    private Vector3 mouseStartPos;

    /// <summary>
    /// The transform's position when the mouse was last pressed down
    /// </summary>
    private Vector3 transformStartPos;

    private bool isBeingClicked = false;

    // Update is called once per frame
    void Update()
    {
        if (isBeingClicked)
        {
            if (IsMouseDown() && !mouseDown)
            {
                mouseStartPos = GetMousePos();
                transformStartPos = transform.position;
                mouseDown = true;
            }
            else if (!IsMouseDown())
            {
                mouseDown = false;  //TODO do something when the input ends so that the unit returns to a state of not being picked up
            }

            if (mouseDown)
            {
                Vector3 mousePos = GetMousePos();
                Vector3 delta = (mousePos - mouseStartPos) * Sensitivity;

                //For this game, we only want units moving in the x and z directions
                transform.position = new Vector3(transformStartPos.x + delta.x, transformStartPos.y, transformStartPos.z + delta.z);
            }
        }
    }

    private bool IsMouseDown() => Input.GetMouseButton(0);
    private Vector3 GetMousePos()
    {
        Ray ray = InputCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 pos = Vector3.zero;
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
        }

        return pos;
    }

    private void OnMouseDown()
    {
        isBeingClicked = true;
        Debug.Log("Clicked This object");
    }

    private void OnMouseUp()
    {
        isBeingClicked = false;
        Debug.Log("Stopped clicking this object");
    }
}
