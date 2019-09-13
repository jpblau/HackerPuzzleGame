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

    private Vector3 home;
    public Target crownJewelsReturnLocation;

    public GameObject heldItem; // An item that this unit can walk around with

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

        home = this.transform.position;     //TODO set this when the player places the unit
    }

    // Update is called once per frame
    void Update()
    {
        
        // If we're not actively supposed to be moving, then don't go to our destination
        if (!nav.isStopped && !(GetAAUnitState().Equals(State.Moving)))
        {
            StopNavMesh();
        }

        // If our target moves (and we're not stopped), update our destination based on its position
        if (!nav.isStopped && doesTargetMove)
        {
            SetDestination(target.GetLocation());
        }

        #region Held Item
        if (heldItem)
        {
            SetTarget(crownJewelsReturnLocation);   //TODO doing this every update cycle is pretty sub-optimal
        }
        #endregion

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
                this.AAUnitState = State.Dead;
                //Destroy(this.gameObject);
                break;
        }
    }

    /// <summary>
    /// This function returns the Unit to it's home location and sets its state to idle
    /// To be called at during the Reset State
    /// 
    /// </summary>
    public void Reset()
    {
        this.gameObject.transform.position = home;
        nav.nextPosition = home;
        this.AAUnitState = State.Idle;
        Debug.Log("Unit Reset");
    }

    /// <summary>
    /// This is the method for when this unit needs to be permanently removed
    /// </summary>
    public void PermaDeath()
    {

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

    /// <summary>
    /// Because setting isStopped just isn't enough, apparently
    /// </summary>
    private void StopNavMesh()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
    }


    /// <summary>
    /// Drops the current held item, and makes heldItem null
    /// </summary>
    private void DropHeldItem()
    {
        heldItem.GetComponent<HeldItem>().Drop();
        heldItem = null;
    }


}
