using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningMachineSpawnerTool : InventoryItem { // This might become a turret of some kind

    [SerializeField] private GameObject miningMachinePrefab;
    [SerializeField] private Vector3 MachinePositionOffset;

    private bool inRangeOfGagNode;
    private GameObject GagNode;
    private BlueGagNode node;

    private Animator playerAnimator;

    private int cost;  

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();

        cost = miningMachinePrefab.GetComponent<MiningMachine>().Cost;
    }
    

    private bool CanPlayerPayForMachine()
    {
        //return GameManager.Instance.PlayerResources.CurrentResources >= cost;

        if (GameManager.Instance.PlayerResources.CurrentResources >= cost)
        { 
            return true;
        }
        else
        {
            Debug.Log("Player can NOT pay for " + miningMachinePrefab.name);
            return false;
        }
    }

    public override void Action()
    {
        if(inRangeOfGagNode && CanPlayerPayForMachine() && !node.HasMiningMachine)
        {
            playerAnimator.SetTrigger("Build");
        }
        else
        {
            Debug.Log(" Is Player within range (" + inRangeOfGagNode + ") " +
                "Can player Pay for Machine(" + CanPlayerPayForMachine() + ") " + 
                " Node Has miningMachine (" + node.HasMiningMachine + ") ");
        }
        //SpawnMiningMachine();
    }

    private void SpawnMiningMachine()
    {
        if (GagNode != null)
        {
           
            if (!node.HasMiningMachine)
            {
                node.HasMiningMachine = true;
                var machineClone = Instantiate(miningMachinePrefab, GagNode.transform);
                machineClone.transform.position = GagNode.transform.position + MachinePositionOffset;

                GameManager.Instance.PlayerResources.CurrentResources -= cost;
            }
            else
            {
                Debug.LogWarning("InRangeofNode(" + inRangeOfGagNode + ") Node Has Machine(" + node.HasMiningMachine + ") --> Make UI to tell Player" );
            }
        }
        else
        {
            Debug.Log("You are CALLING SPAWN MACNINE  but GagNode Gameobject is empty");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if (collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            Debug.Log("Inrage of GagNode !!!!!!!!!!");
            inRangeOfGagNode = true;
            GagNode = collision.gameObject;
            node = GagNode.GetComponent<BlueGagNode>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if (collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            inRangeOfGagNode = false;
            GagNode = null;
            node = null;
        }
    }
}
