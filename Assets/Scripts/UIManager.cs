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
    public Canvas canv_LevelCanvasUI;
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
    public void SetVis_StartRoundUI(bool enabled)
    {
        canv_startRoundUI.enabled = !canv_startRoundUI.enabled;
    }

    public void SpinButtonPressed()
    {
        SetVis_StartRoundUI(false);
        LM.EndStart();
        
    }
    #endregion


    #region Placement State
    public void SetVis_PlacementUI(bool enabled)
    {
        canv_placementUI.enabled = !canv_placementUI.enabled;
    }

    public void BeginRoundButtonPressed()
    {
        SetVis_PlacementUI(false);
        LM.EndPlacement(); 
    }
    #endregion

    #region Complete State
    public void SetVis_LevelWinUI(bool enabled)
    {
        canv_winUI.enabled = enabled;
    }

    public void SetVis_LevelLoseUI(bool enabled)
    {
        canv_loseUI.enabled = enabled;
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


    public void SetVis_LevelCanvasUI(bool enabled)
    {
        canv_LevelCanvasUI.enabled = enabled;
    }

    // TODO NEED A NEWLEVELLOADED FUNCTION THAT WILL UPDATE ALL THE UI ELEMENTS WITH VALUES FROM THE LEVELMANAGER
    public void NewLevelWasLoaded(LevelManager lm, int roundNumber)
    {
        this.LM = lm;
        SetRoundText(roundNumber);
        SetVis_LevelCanvasUI(true);
    }
}
