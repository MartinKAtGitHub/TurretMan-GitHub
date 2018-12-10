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

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();          
    }
    
    public override void Action()
    {
        if(inRangeOfGagNode)
        {
            playerAnimator.SetTrigger("Build");
        }
        else
        {
            Debug.Log("Not in Range of GagNode to create Mining Machine");
        }
        //SpawnMiningMachine();
    }

    private void SpawnMiningMachine()
    {
        if (GagNode != null)
        {

            node =  GagNode.GetComponent<BlueGagNode>();
            if (!node.HasMiningMachine)
            {
                    node.HasMiningMachine = true;
                    var machineClone = Instantiate(miningMachinePrefab, GagNode.transform);
                    machineClone.transform.position = GagNode.transform.position + MachinePositionOffset;
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
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if (collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            inRangeOfGagNode = false;
            GagNode = null;
        }
    }
}
