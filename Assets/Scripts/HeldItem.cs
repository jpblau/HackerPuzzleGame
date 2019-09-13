using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    public enum ItemType { CrownJewels }
    public ItemType type;

    private GameObject owner;
    public bool isOwned;
    private Vector3 home;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (owner)
        {
            FollowOwner();
        }
    }

    /// <summary>
    /// When a Unit enters this item's collision radius, if that unit can pick up this item, 
    /// have them pick up this item.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        AAUnit otherUnit = other.GetComponent<AAUnit>();
        if (otherUnit && otherUnit.heldItem == null)
        {
            otherUnit.heldItem = this.gameObject;
            PickUp(other.gameObject);
        }

        //TODO HERE don't just automatically attach the item-- the unit should determine whether it wants the item or not. 
        // something like if "otherUnit.WantsItem(), then attach it"
    }


    private void PickUp(GameObject other)
    {
        owner = other.gameObject;
        isOwned = true;
        Debug.Log(this + "picked up by: " + other);
    }


    /// <summary>
    /// Return this unit to its home location and set its ownership to null
    /// </summary>
    public void Drop()
    {
        this.gameObject.transform.position = home;
        owner = null;
        isOwned = false;
    }

    /// <summary>
    /// Called once an update frame, if this unit has an owner
    /// </summary>
    public void FollowOwner()
    {
        Vector3 newPos = owner.transform.position;
        this.transform.position = new Vector3(newPos.x, newPos.y + 1.0f, newPos.z);
    }
}
