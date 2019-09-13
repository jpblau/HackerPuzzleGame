using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnLocation : MonoBehaviour
{
    private LevelManager LM;

    // Start is called before the first frame update
    void Start()
    {
        LM = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the unit we've collided with is holding the crown jewels, end the level
        AAUnit unit = other.GetComponent<AAUnit>();
        if (unit && unit.heldItem.GetComponent<HeldItem>().type.Equals(HeldItem.ItemType.CrownJewels))
        {
            LM.PlayerWin();
        }
    }
}
