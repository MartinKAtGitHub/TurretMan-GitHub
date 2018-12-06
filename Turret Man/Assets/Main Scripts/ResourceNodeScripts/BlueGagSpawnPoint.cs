using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class BlueGagSpawnPoint : MonoBehaviour {

    BlueGagsSpawner BGS;

    // I dont think this should be done IN game --> the ref should be set up in the Editor but i am not sure of to set it up
    private void Awake()
    {
        BlueGagsSpawner.SpawnPoints.Add(gameObject);
    }

    private void OnDestroy()
    {
        BlueGagsSpawner.SpawnPoints.Remove(gameObject);
    }
}
