using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour {

    public List<InventoryItem> inventoryItems;

    [SerializeField]private InventoryItem selectedInventoryItem;

	void Start ()
    {
        // inventoryItems.AddRange( GetComponentsInChildren<InventoryItem>() );
         inventoryItems.AddRange( GetComponents<InventoryItem>() );
        PairInventoryItemsToInputKeys();
    }
	
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetInventoryItemToSlected(inventoryItems[0]);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {

            SetInventoryItemToSlected(inventoryItems[1]);
          
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetInventoryItemToSlected(inventoryItems[2]);
      
        }

        if( (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.KeypadEnter))  /* && inventoryKeyLoock == false */)
        {
            GetSelectedInventoryItemAction();
        }
    }


    public void PairInventoryItemsToInputKeys()
    {
        inventoryItems.Sort();
    }

  
    private void SetInventoryItemToSlected(InventoryItem item)
    {
        if(selectedInventoryItem == item)
        {
            GetSelectedInventoryItemAction();
        }
        else
        {
            selectedInventoryItem = item;
        }

        //selectedInventoryItem = item;
    }

    private void GetSelectedInventoryItemAction()
    {
        if(selectedInventoryItem == null)
        {
            Debug.LogWarning("NO ITEM WAS SELECTED BUT ACTION WAS USED");
            return;
        }else
        {
            selectedInventoryItem.Action();
        }
    }
}
