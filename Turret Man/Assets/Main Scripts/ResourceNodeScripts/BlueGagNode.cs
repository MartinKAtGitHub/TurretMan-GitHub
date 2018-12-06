using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGagNode : ResourceNode {


    // Use this for initialization
    new void Start ()
    {
        base.Start();
        Debug.Log("BlueNode Start()");
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(CurrentResourceAmount == 0)
        {
            NodeDepleted();
        }
	}

    public override int MineResource(int miningPower)
    {
        //DO we want check to retunr; if the Node has 0 resources--> use full if we want to trigger anim or somthign els

        var temp = CurrentResourceAmount;
       
        temp -= miningPower;

        if (temp <= 0)
        {
            var remainingResources =  CurrentResourceAmount;
            Debug.Log("remainingResources = " + remainingResources);
            CurrentResourceAmount = 0;

            NodeDepleted();// Node depeleted --> event

            return remainingResources;
        }
        
        Debug.Log("Temp Res = " + temp);
        var diff =  CurrentResourceAmount - temp;
        CurrentResourceAmount -= diff;

        //Debug.Log("DIFF = " + diff);

        return diff;
    }

}
