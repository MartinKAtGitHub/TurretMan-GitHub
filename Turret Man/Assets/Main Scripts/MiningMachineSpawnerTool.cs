using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningMachineSpawnerTool : InventoryItem { // This might become a turret of some kind

    [SerializeField] private GameObject miningMachinePrefab;
    [SerializeField] private Vector3 MachinePositionOffset;

    private bool inRangeOfGagNode;
    private GameObject GagNode;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public override void Action()
    {
        SpawnMiningMachine();
    }

    private void SpawnMiningMachine()
    {
        Debug.Log("SPAWING MACHINE ACTION");

        if (GagNode != null)
        {

            var nodeOcciped =  GagNode.GetComponent<BlueGagNode>().HasMiningMachine;
            if (inRangeOfGagNode && !nodeOcciped)
            {
                    nodeOcciped = true;
                    var machineClone = Instantiate(miningMachinePrefab, GagNode.transform);
                    machineClone.transform.position = new Vector3(0,0,0) + MachinePositionOffset; // Dosent Work need to reset
            }
            else
            {
                Debug.LogWarning("InRangeofNode(" + inRangeOfGagNode + ") Node Has Machine(" + nodeOcciped + ") --> Make UI to tell Player" );
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
