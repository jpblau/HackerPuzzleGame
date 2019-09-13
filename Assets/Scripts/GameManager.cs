using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Don't Destroy
    private static GameManager dontDestroy; // Ensures that there is only one instance of this gameObject
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

    private enum State { MainMenu, Level}
    private State GameState;

    public UIManager UIM;

    // Start is called before the first frame update
    void Start()
    {
        this.GameState = State.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePos();
    }

    private Vector3 GetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 pos = Vector3.zero;
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
            Debug.Log(hit.transform);
        }

        return pos;
    }

    /// <summary>
    /// Load the provided scene, and then call any onLevelLoaded functions in other Managers
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadNewScene(string sceneName)
    {
        Debug.Log("Loading Scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// This should only be called from button presses on the level select screen
    /// </summary>
    public void NewLevelWasLoaded()
    {
        this.GameState = State.Level;
    }

    /// <summary>
    /// Called by LevelManger at the end of a level, returning the player to the Main Menu scene 
    /// with the level select screen active
    /// </summary>
    public void LevelComplete()
    {
        this.LoadNewScene("MainMenu");
        UIM.SetVis_LevelCanvasUI(false);
        UIM.SetVis_LevelSelectUI(true);
        this.GameState = State.MainMenu;
        
    }
}
