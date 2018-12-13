using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class BlueGagsSpawner : ResourceNodeSpawner {

    private BlueGagNode blueGagNodePrefab;

    private void Awake()
    {
        // GameManager.Instance.BlueGagsSpawner = this;
        GetAllBlueGagSpawnPoints();
    }

    void Start ()
    {
        blueGagNodePrefab = ResourceNodeToSpawnPrefab.GetComponent<BlueGagNode>();
        blueGagNodePrefab.Spawner = this; //Maybe crate Init Method --> also this is connected to prefab Mayeb do this afther instantiate and conect to clone

        InvokeRepeating("SpawnBlueGag", 0, seconds);
	}
    

    void SpawnBlueGag()
    {
       // Debug.Log("Checking blueGag Nodes and SPAWNING");
        if(GagsOnMapCounter <= MinMaxGagLimits.x)
        {
            //SpawnNewBlueGagNodeRecoursive();
            SpawnNewBlueGagNode();
        }
        /*  else if(GagsOnMapCounter >= MinMaxGagLimits.y)
          {
              Debug.Log("Blue Gags (" + GagsOnMapCounter + ")" + " > " + MinMaxGagLimits.y + " BREAK");

              return;
          }*/
    }

    private void SpawnNewBlueGagNode()
    {
        //Debug.Log("Blue Gags (" + GagsOnMapCounter + ")" + " < " + MinMaxGagLimits.x + " Spawning more");

        if(SpawnPoints.Count != 0)
        {
            var ranIndex = Random.Range(0, SpawnPoints.Count);
      
            var node = Instantiate(blueGagNodePrefab, SpawnPoints[ranIndex].gameObject.transform.position , Quaternion.identity);

            SpawnPointstOccupied.Add(SpawnPoints[ranIndex]);
            node.SpawnPoint = SpawnPoints[ranIndex];
            SpawnPoints.Remove(SpawnPoints[ranIndex]);

            GagsOnMapCounter++;
        }
        else
        {
            Debug.LogWarning("NO avaliable spawnLocations for = " + this.GetType());
        }
    }
    
    private void SpawnNewBlueGagNodeRecoursive()
    {
        Debug.Log("Blue Gags (" + GagsOnMapCounter + ")" + " < " + MinMaxGagLimits.x + " Spawning more");
        var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
        if (!spawnPoint.SpawnPointOccupied)
        {
            // Rmoveit And Add it to other list
            Instantiate(blueGagNodePrefab, spawnPoint.gameObject.transform);
            spawnPoint.SpawnPointOccupied = true;
            GagsOnMapCounter++;
        }
        else
        {
            Debug.LogWarning("BlueGAG SpawnPoint Occupied = " + spawnPoint.SpawnPointOccupied + "Roll Again");
            for (int i = 0; i < SpawnPoints.Count; i++)
            {
                if(!SpawnPoints[i].SpawnPointOccupied)
                {
                    // SpawnNewBlueGagNodeRecoursive(); // maybe cause infinit loop AND Performance hits
                    break;
                }
            }
            Debug.LogError("Not Enough BlueGags SpawnPoints");
        }
    }

   
    private void GetAllBlueGagSpawnPoints()
    {
        //if(blue)
        SpawnPoints.AddRange(GetComponentsInChildren<BlueGagSpawnPoint>());
        //if(green)
        //SpawnPoints.AddRange(GetComponentsInChildren<GreemGagsSpawnPoint>());
    }
}
