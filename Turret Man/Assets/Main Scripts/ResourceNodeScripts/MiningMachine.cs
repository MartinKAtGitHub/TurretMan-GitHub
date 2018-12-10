using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningMachine : MonoBehaviour {

    public int MaxHP;
    public int CurrentHP;
    public int Cost;
    /// <summary>
    /// How meny resources this machine will mine every X amount of time
    /// </summary>
    public int MiningMachinePower;
    /// <summary>
    /// How many sec betwwen every mining action
    /// </summary>
    [SerializeField]private int miningRateInSec;

    private ResourceNode GagNode;
	// Use this for initialization
	void Start ()
    {
        GagNode = GetComponentInParent<ResourceNode>();
        if (GagNode == null)
        {
            Debug.LogError("MiningMachine Exist but not Attached to a GagNode So the mechine is mining -> NULL");
            return;
        }
            InvokeRepeating("MinigAction", miningRateInSec, miningRateInSec);
	}
	
    private void MinigAction()
    {
       GameManager.Instance.PlayerResources.CurrentResources += GagNode.MineResource(MiningMachinePower);
    }
}
