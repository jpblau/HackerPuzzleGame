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

    public UIManager UIM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    /// Called by LevelManger at the end of a level, returning the player to the Main Menu scene 
    /// with the level select screen active
    /// </summary>
    public void LevelComplete()
    {
        this.LoadNewScene("MainMenu");
        UIM.SetVis_LevelSelectUI(true);
    }
}
