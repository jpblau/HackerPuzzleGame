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
    public Canvas canv_LevelSelectUI;
    public Canvas canv_startRoundUI;
    public Canvas canv_placementUI;
    public Canvas canv_winUI;
    public Canvas canv_loseUI;
    private LevelManager LM;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Start State
    /// <summary>
    /// Enables and resets all the UI that starts a round
    /// </summary>
    public void ToggleStartRoundUI()
    {
        canv_startRoundUI.enabled = !canv_startRoundUI.enabled;
    }

    public void SpinButtonPressed()
    {
        ToggleStartRoundUI();
        LM.EndStart();
        
    }
    #endregion


    #region Placement State
    public void TogglePlacementUI()
    {
        canv_placementUI.enabled = !canv_placementUI.enabled;
    }

    public void BeginRoundButtonPressed()
    {
        TogglePlacementUI();
        LM.EndPlacement(); 
    }
    #endregion

    #region Complete State
    public void ToggleLevelWinUI()  //TODO change all these to take a bool as a param, and set enabled to that value
    {
        canv_winUI.enabled = !canv_winUI.enabled;
    }

    public void ToggleLevelLoseUI()
    {
        canv_loseUI.enabled = !canv_loseUI.enabled;
    }
    #endregion

    #region Level Select Screen
    public void SetVis_LevelSelectUI(bool enabled)
    {
        canv_LevelSelectUI.enabled = enabled;
    }
    #endregion

    /// <summary>
    /// Sets the text that keeps track of the current round number
    /// </summary>
    /// <param name="roundNumber"></param>
    public void SetRoundText(int roundNumber)
    {
        roundText.text = roundNumber.ToString();
    }


    // TODO NEED A NEWLEVELLOADED FUNCTION THAT WILL UPDATE ALL THE UI ELEMENTS WITH VALUES FROM THE LEVELMANAGER
    public void SetLevelManagerValues(LevelManager lm)
    {
        this.LM = lm;
    }
}
