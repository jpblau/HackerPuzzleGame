using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    #region Don't Destroy
    private static UIManager dontDestroy; // Ensures that there is only one instance of this gameObject
    private void Awake()
    {
        DontDestroyBetweenScenes();
    }

    private void DontDestroyBetweenScenes()
    {
        // Ensure taht this is the only object we are not destroying, and that we create no duplicates
        if (dontDestroy == null)
        {
            DontDestroyOnLoad(this.gameObject); // We don't want this object to be destroyed any time we change scenes
            dontDestroy = this;
        }
        else if (dontDestroy != this)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public Text roundText;
    public Canvas canv_startRoundUI;
    private LevelManager LM;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Enables and resets all the UI that starts a round
    /// </summary>
    public void ToggleStartRoundUI()
    {
        canv_startRoundUI.enabled = !canv_startRoundUI.enabled;
    }

    public void SpinButtonPressed()
    {
        LM.EndStart();
        ToggleStartRoundUI();
    }

    /// <summary>
    /// Sets the text that keeps track of the current round number
    /// </summary>
    /// <param name="roundNumber"></param>
    public void SetRoundText(int roundNumber)
    {
        roundText.text = roundNumber.ToString();
    }


    // NEED A NEWLEVELLOADED FUNCTION THAT WILL UPDATE ALL THE UI ELEMENTS WITH VALUES FROM THE LEVELMANAGER
    public void SetLevelManagerValues(LevelManager lm)
    {
        this.LM = lm;
    }
}
