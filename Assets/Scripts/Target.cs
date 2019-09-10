using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Anything in the game that can become a target for a unit to move towards
/// </summary>
public class Target : MonoBehaviour
{
    public bool moves;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Get the location of this Target's gameobject
    /// </summary>
    /// <returns>The current location of this target</returns>
    public Vector3 GetLocation()
    {
        return this.gameObject.GetComponent<Transform>().position;
    }


    public void SetIsActive(bool value)
    {
        isActive = value;
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
