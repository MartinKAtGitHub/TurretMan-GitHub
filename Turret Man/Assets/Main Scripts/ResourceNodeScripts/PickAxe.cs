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
    Animator playerAnimator;



    private void Awake()
    {
        playerResources = GetComponentInParent<PlayerResources>();
        playerAnimator = GetComponentInParent<Animator>();
    }
    
    
    public override void Action()
    {
        Debug.Log("Action Using =" + ItemName);
        playerAnimator.SetTrigger("UsePickAxe");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if(collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            playerResources.CurrentResources += collision.GetComponent<ResourceNode>().MineResource(PickAxePowerLevel);
        }
    }

   
}
