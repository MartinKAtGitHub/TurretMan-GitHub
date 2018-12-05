using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class BlueGagsSpawner : ResourceNodeSpawner {

    public static List<GameObject> SpawnPoints = new List<GameObject>();
    //list of BlueGagSpawnPoint Transforms
   
    // Use this for initialization
    void Start ()
    {
        

      /*  for (int i = 0; i < SpawnPoints.Count; i++)
        {
            Debug.Log( " TRANS | "+ SpawnPoints[i].transform.position); 
        }*/
	}
	
	// Update is called once per frame
	void Update ()
    {
		// Timer
	}

    void SpawnBlueGag()
    {
        // if(counter < min(5))
            // insta ()
    }
}
