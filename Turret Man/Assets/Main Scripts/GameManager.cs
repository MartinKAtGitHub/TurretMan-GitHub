using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [HideInInspector]public PlayerResources PlayerResources;
    //public GameObject WinPnl;
    //public GameObject MainMenu;
    public GameObject EscPnl;

    public static GameManager Instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    public delegate void GameMangerEventHandler();

    public event GameMangerEventHandler PlayerWinEvent;
    public event GameMangerEventHandler MenueToggle;
    public event GameMangerEventHandler RestartEvent;
    public event GameMangerEventHandler GameOverEvent;


    public int GagWinCondition;
    public int GagStartAmount;
    private bool isGamePaused;
    public bool Victory;
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            Instance = this;
        }
        //if instance already exists and it's not this
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        PlayerResources = GetComponent<PlayerResources>();

       // WinPnl.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoade;
    }


    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Debug.Log("Esc MENU");
            PauseGame();
        }

    }


    public void PauseGame()
    {
        if(isGamePaused)
        {
            isGamePaused = false;
            EscPnl.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isGamePaused = true;
            EscPnl.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void CallWinEvent()
    {
        if(PlayerWinEvent != null)
        {
            PlayerWinEvent();
        }
    }


    public void GameWin()
    {
        // Time.timeScale = 0f; // pause game
        //  WinPnl.SetActive(true);

        // Start Game Win UI
        CallWinEvent();
        Victory = true;
    }

    public void ReStartLevel()
    {
        Debug.Log("Restarting Level");
        this.PlayerResources.CurrentResources = GagStartAmount;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Go to game main menu screen");
    }

    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene(1);
    }

    private void OnSceneLoade(Scene scene,LoadSceneMode mode)
    {
        
      // Time.timeScale = 1f;
       // WinPnl.SetActive(false);
        

      /* if (SceneManager.GetActiveScene().buildIndex != 0)
        {

           MainMenu.SetActive(false);
        }*/
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoade;
    }
}
