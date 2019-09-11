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

    public enum State { Start, Placement, ActiveRound, Reset, Complete}
    public State LevelState;

    [SerializeField]
    private UIManager UIM;

    // Start is called before the first frame update
    void Start()
    {
        roundNumber = 0;
        UIM = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIManager>();
        UIM.SetLevelManagerValues(this);

        //redTeamUnits = new List<AAUnit>();
        blueTeamUnits = new List<AAUnit>();

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
    }

    #region Start State Functions
    /// <summary>
    /// Begins the Start State
    /// </summary>
    private void BeginStart()
    {
        UIM.ToggleStartRoundUI();
    }


    /// <summary>
    /// Ends the Start State
    /// </summary>
    public void EndStart()
    {
        SetLevelState(State.ActiveRound);       //TODO CURENTLY STARTS THE ACTIVEROUND RATHER THAN GOING TO PLACEMENT
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
    }
}
