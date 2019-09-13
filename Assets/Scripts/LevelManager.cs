using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The LevelManager keeps track of any level-specific numbers, such as the number of rounds, units, money, etc.
/// TODO this LevelManager's start method should serve as an old OnLevelWasLoaded() call, notifying the GameManager that this level has loaded and is prepped to go.
/// </summary>
public class LevelManager : MonoBehaviour
{
    private int roundNumber;
    public List<AAUnit> redTeamUnits = new List<AAUnit>();
    private List<AAUnit> blueTeamUnits;

    public enum State { Start, Placement, ActiveRound, Reset, Complete }
    public State LevelState;

    private bool areCrownJewelsHome = true;
    private bool hasPlayerWon = false;

    [SerializeField]
    private UIManager UIM;

    // Start is called before the first frame update
    void Start()
    {
        roundNumber = 1;
        

        //redTeamUnits = new List<AAUnit>();
        blueTeamUnits = new List<AAUnit>();


        UIM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        UIM.NewLevelWasLoaded(this, roundNumber);

        SetLevelState(State.Start);
        BeginStart();
    }

    // Update is called once per frame
    void Update()
    {
        #region START

        #endregion

        #region ROUNDACTIVE
        if (LevelState.Equals(State.ActiveRound))
        {
            bool shouldEndRound = true;
            // loop through all the red team units, and if no more are active, then end the round
            for (int i = 0; i < redTeamUnits.Count; i++)
            {
                if (redTeamUnits[i].GetAAUnitState().Equals(AAUnit.State.Moving))
                {
                    shouldEndRound = false;
                    break;
                }
            }
            if (shouldEndRound)
            {
                EndRound();
            }
        }
        #endregion

        #region RESET
        #endregion
    }

    #region Start State Functions
    /// <summary>
    /// Begins the Start State
    /// </summary>
    private void BeginStart()
    {
        UIM.SetVis_StartRoundUI(true);
    }


    /// <summary>
    /// Ends the Start State
    /// </summary>
    public void EndStart()
    {
        SetLevelState(State.Placement);  
        BeginPlacement();
    }
    #endregion

    #region Placement State Functions
    private void BeginPlacement()
    {
        UIM.SetVis_PlacementUI(true);
    }

    public void EndPlacement()
    {
        SetLevelState(State.ActiveRound);
        BeginRound();
    }
    #endregion

    #region ActiveRound State Functions
    /// <summary>
    /// Start the current round, and get all the units moving
    /// </summary>
    private void BeginRound()
    {
        // go through the red team and get them all moving TODO do this for the blue team as well
        for (int i = 0; i < redTeamUnits.Count; i++)
        {
            redTeamUnits[i].BeginActiveRound();
        }
    }

    /// <summary>
    /// Ends the current round, and updates any necessary functions
    /// </summary>
    public void EndRound()
    {
        roundNumber++;
        UIM.SetRoundText(roundNumber);
        SetLevelState(State.Reset);
        BeginReset();
    }

    #endregion

    #region Reset State Functions
    /// <summary>
    /// This function should take every unit and resets it
    /// </summary>
    private void BeginReset()
    {
        // Reset all the red team members
        for (int i = 0; i < redTeamUnits.Count; i++)
        {
            redTeamUnits[i].Reset();
        }
        //TODO should also change fog of war values back

        EndReset();
    }

    private void EndReset()
    {
        SetLevelState(State.Placement);
        BeginPlacement();
    }
    #endregion

    #region Complete State Functions
    /// <summary>
    /// Called when the player has won the level
    /// </summary>
    public void PlayerWin()
    {
        hasPlayerWon = true;
        SetLevelState(State.Complete);
        BeginComplete();
    }

    /// <summary>
    /// Called when the player has definitively lost the level
    /// </summary>
    public void PlayerLose()
    {

    }

    // Only called once the player has won or lost
    private void BeginComplete()
    {
        if (hasPlayerWon)
        {
            UIM.SetVis_LevelWinUI(true);
        }
        else
        {
            UIM.SetVis_LevelLoseUI(true);
        }

        // We wait to call EndComplete until the user hits a button that takes them back to the level select screen
    }

    /// <summary>
    /// Called when the player hits a "back to level select" button after completing the level
    /// </summary>
    public void EndComplete()
    {
        GameManager GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        GM.LevelComplete();
        //TODO I DONT THINK THIS FUNCTION ACTUALLY EVER GETS CALLED
    }

    #endregion

    /// <summary>
    /// Set our LevelState to the given value
    /// </summary>
    /// <param name="newState">The new state</param>
    private void SetLevelState(State newState)
    {
        LevelState = newState;
        Debug.Log("Our Current Level State is: " + LevelState);

        //TODO ALL THE BEGINPLACEMENT AND BEGINROUND FUNCTIONS SHOULD BE CALLED FROM A SWITCH STATEMENT IN HERE, NOT FROM INDIVIDUAL FUNCTIONS
    }
}
