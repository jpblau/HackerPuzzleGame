using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AAUnits are Autonomous Agent units that use Unity's NavMesh system to navigate to their objectives.
/// Each AAUnit must have a NavMeshAgent
/// </summary>
public class AAUnit : MonoBehaviour
{
    [SerializeField]
    private Target target;   // This is only public because we'd want to see this in the inspector
    private bool doesTargetMove;

    private NavMeshAgent nav;
    
    public enum State { Shelved, Idle, Moving, Gathering, Dead}
    public State AAUnitState;

    private Vector3 placedPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.nav = this.GetComponent<NavMeshAgent>();

        // If we start with a target, set it as our target
        if (target)
        {
            SetTarget(target);
        }

        // Set our unit's initial state
        AAUnitState = State.Idle;
        nav.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If we're not actively supposed to be moving, then don't go to our destination
        /*if (!nav.isStopped && !(GetAAUnitState().Equals("Moving")))
        {
            nav.isStopped = true;
        }*/

        // If our target moves (and we're not stopped), update our destination based on its position
        if (!nav.isStopped && doesTargetMove)
        {
            SetDestination(target.GetLocation());
        }
    }

    /// <summary>
    /// Set this AAunit's target-- the object that they will be moving towards.
    /// This target can be another AAunit, even one that is constantly moving. 
    /// </summary>
    public void SetTarget(Target newTarget)
    {
        this.target = newTarget;
        this.doesTargetMove = newTarget.moves;
        SetDestination(target.GetLocation());
    }

    /// <summary>
    /// The destination for an AAUnit is a static location. For moving objects, this is updated every update cycle
    /// </summary>
    public void SetDestination(Vector3 newLocation)
    {
        nav.SetDestination(newLocation);
    }

    /// <summary>
    /// The method called when this unit needs to be removed from the round.
    /// This does not permanently destroy the unit, it just removes the unit from the current session
    /// </summary>
    public void Death(string reason)
    {
        switch (reason)
        {
            case "firewall":
                Destroy(this.gameObject);
                break;
        }
    }

    public State GetAAUnitState()
    {
        return this.AAUnitState;
    }

    /// <summary>
    /// The method called when a round begins, which gets this unit moving towards its destination
    /// </summary>
    public void BeginActiveRound()
    {
        this.AAUnitState = State.Moving;
        nav.isStopped = false;
    }


}
