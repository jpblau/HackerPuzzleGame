using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Material dissolveMat;
    public float minDissolveAmount = -1.0f;  // Goes from -1 (not dissolved at all)->1 (totally dissolved)
    public float maxDissolveAmount = 1.0f;
    public float dissolveIncrement = .25f;
    public bool hasDissolvedOnce;   // Has this fogOfWar unit dissolved at least once yet?

    // Start is called before the first frame update
    void Start()
    {
        dissolveMat.SetFloat("Vector1_DB6E3BEF", minDissolveAmount);
        hasDissolvedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasDissolvedOnce)   // TODO THIS WILL ACTIVATE, BUT WILL NEVER ACTUALLY EFFECT THE MATERIAL SINCE THE SHADER IS STILL USING HARDCODED COLORS
        {
            minDissolveAmount = -.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Dissolve();
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        //UnDissolve();
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;

    }


    public void Dissolve()
    {
        StartCoroutine("Crouton_Dissolve");
        Debug.Log("Dissolving");
    }

    public void UnDissolve()
    {
        StartCoroutine("Crouton_UnDissolve");
        Debug.Log("UnDissolving");
    }

    IEnumerator Crouton_Dissolve()
    {
        for (float f = minDissolveAmount; f <= maxDissolveAmount; f += dissolveIncrement)
        {
            dissolveMat.SetFloat("Vector1_DB6E3BEF", f / 10000);
            //yield return new WaitForSeconds(.0000001f);
        }
        hasDissolvedOnce = true;
        yield return null;
    }

    IEnumerator Crouton_UnDesolve()
    {
            for (float f = maxDissolveAmount; f >= minDissolveAmount; f -= dissolveIncrement)
            {
                dissolveMat.SetFloat("Vector1_DB6E3BEF", f);
                //yield return new WaitForSeconds(.0000001f);
        }
            yield return null;
    }
}
