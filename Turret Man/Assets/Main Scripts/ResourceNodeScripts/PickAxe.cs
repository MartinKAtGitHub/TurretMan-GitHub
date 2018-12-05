using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxe : MonoBehaviour {


    public int MYGAGS;

    /// <summary>
    /// PowerLevel determines how many resources are gathered per mining action
    /// </summary>
    public int PickAxePowerLevel;

    // PlayerResouceManager


    //target RedGag
    //target GreenGag
    //target BlueGag

    // Input manager --> if LMB && anim end(Use anim event OR anim clip lengt) --> execite anim

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("PickAxe ENTER -> " + collision.name);

        if(collision.tag == "BlueGag") // TODO This needs to be changes to somthing less hard coded --> maybe check the layer insted? OR Add targets for pickaxe to check against
        {
            MYGAGS += collision.GetComponent<ResourceNode>().MineResource(PickAxePowerLevel);
        }
    }
    
}
