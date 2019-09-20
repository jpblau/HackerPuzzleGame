using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotReel : MonoBehaviour
{
    public GameObject objectToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown()
    {
        GameObject.Instantiate(objectToInstantiate, new Vector3(0, 0, 0), Quaternion.identity);
        Debug.Log("Instantiated an object");
    }
}
