using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Firewall : MonoBehaviour
{
    private bool isActive;
    private NavMeshObstacle navObs;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        navObs = this.GetComponent<NavMeshObstacle>();
        mesh = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Turn this firewall on, both visually, and for pathing.
    /// </summary>
    public void ActivateFirewall()
    {
        navObs.enabled = true;
        navObs.carving = true;
        mesh.enabled = true;
        isActive = true;
    }

    /// <summary>
    /// Determine what happens to units that walk into the firewall
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        // If the firewall isn't active yet
        if (!isActive)
        {
            ActivateFirewall();
        }

        // If the firewall is active (or, once it's active), kill the thing that bumped into it
        other.gameObject.GetComponent<AAUnit>().Death("firewall");
    }


}
