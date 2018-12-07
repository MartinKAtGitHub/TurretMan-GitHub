using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNode : MonoBehaviour
{
    /// <summary>
    /// The Max/base amount of resources this type of Resource Node has.
    /// </summary>
    [SerializeField] protected int maxResourceAmount;
    /// <summary>
    /// The number which is used to dictate the value of the resource node. This number will be multiplied with the value of 1 Gag (rarity(1) -> 1*1 (rarity(2) -> 1*2  rarity(3) -> 1*3 ) --> Maybe will be chanegs to use anotehr syste
    /// </summary> 
    [SerializeField] protected int resourceRarityModifier;

    /// <summary>
    /// How much resources the resource node has currently.
    /// </summary>
    public int CurrentResourceAmount;

    public ResourceNodeSpawner Spawner;
    public BlueGagSpawnPoint SpawnPoint;


   protected void Start()
    {
        Debug.Log("ResourceNode Start()");

        CurrentResourceAmount = maxResourceAmount;
    }


    public abstract int MineResource(int miningPower);


    protected void NodeDepleted() // Or run on event
    {
        Debug.Log("NODE DEPLETED --> Start DEPELTION ANIM");
        //if(currentre == 0)
        // BreakDownAnim
        // Event
        Spawner.SpawnPointstOccupied.Remove(SpawnPoint);
        Spawner.SpawnPoints.Add(SpawnPoint);

        //SpawnPoint = null;


        Spawner.GagsOnMapCounter --;
        Destroy(gameObject);
    }

    //SpawnNode

    // Collider inRange
}
