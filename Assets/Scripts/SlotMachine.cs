using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{

    public List<GameObject> reels;
    public int cost = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Spins all the reels in this slot machine
    /// </summary>
    public void SpinSlotMachine()
    {
        for (int i = 0; i < reels.Count; i++)
        {
            reels[i].SetActive(true);
        }
    }
}
