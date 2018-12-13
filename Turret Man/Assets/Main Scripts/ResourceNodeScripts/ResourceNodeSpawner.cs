using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNodeSpawner : MonoBehaviour {

    public GameObject ResourceNodeToSpawnPrefab;
   [HideInInspector] public int GagsOnMapCounter;
    public float seconds;
    public Vector2 MinMaxGagLimits;

    //TODO Update List to take any Spawnpoint not only BlueGags
    public  List<BlueGagSpawnPoint> SpawnPoints = new List<BlueGagSpawnPoint>();
    public List<BlueGagSpawnPoint> SpawnPointstOccupied = new List<BlueGagSpawnPoint>();
   
    
    //static int counter; Per childe class 

    // More in the future and maybe simpler setup

    // TODO GagsSpawner Check To see if Collider is in Spawn area to stop spawn or destory anythign there
}
