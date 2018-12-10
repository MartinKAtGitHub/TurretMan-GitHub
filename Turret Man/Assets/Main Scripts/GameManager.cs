using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]public PlayerResources PlayerResources;

    public static GameManager Instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
   
  
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
    }

    //Update is called every frame.
    void Update()
    {

    }
}
