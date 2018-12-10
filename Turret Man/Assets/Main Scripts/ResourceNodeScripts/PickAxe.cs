using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickAxe : InventoryItem {

  
    [Space(10)]
    /// <summary>
    /// PowerLevel determines how many resources are gathered per mining action
    /// </summary>
    public int PickAxePowerLevel;
  
    // PlayerResouceManager


    //target RedGag
    //target GreenGag
    //target BlueGag

    // Input manager --> if LMB && anim end(Use anim event OR anim clip lengt) --> execite anim
    PlayerResources playerResources;
    ResourceNode gageNode;
    Animator playerAnimator;


    private bool canMineNode;

    private void Awake()
    {
        // playerResources = GetComponentInParent<PlayerResources>();

        playerAnimator = GetComponentInParent<Animator>();
    }
    
    
    public override void Action()
    {
        if(canMineNode)
        {
            Debug.Log("Action Using =" + ItemName);
            playerAnimator.SetTrigger("UsePickAxe");
        }else
        {
            Debug.Log("Not in Range of GagNode to Mine with PickAxe");
        }

    }

    /// <summary>
    /// This method is called in the Pickaxe Anim , at the point where the pick striks the ground. This is done incase you want to / cancel the anim you dont get free ore
    /// </summary>
    private void MineResourseNodeWithPickAxeAnimEvent()
    {

        if(gageNode != null)
        {
            GameManager.Instance.PlayerResources.CurrentResources+= gageNode.GetComponent<ResourceNode>().MineResource(PickAxePowerLevel);
        }
        else
        {
            canMineNode = false;
            Debug.LogError("You are Trying to PickAxe Mine a Node which is Null Breaking off");
            return;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if(collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            Debug.Log("Enter Mining Area");
            canMineNode = true;
            //playerResources.CurrentResources += collision.GetComponent<ResourceNode>().MineResource(PickAxePowerLevel);
            // GameManager.Instance.PlayerResources.CurrentResources+= collision.GetComponent<ResourceNode>().MineResource(PickAxePowerLevel);
            gageNode = collision.GetComponent<ResourceNode>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if (collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            Debug.Log("Exit Mining Area");
            canMineNode = false;
            gageNode = null; // IDK if this is a goood idea 
        }
    }


}
