using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour {

    
    public List<InventoryItem> inventoryItems;

    [SerializeField]private InventoryItem selectedInventoryItem;


    public GameObject SelectedItemImage_GO;
    private Image selectedItem_Image;  
    private Animator selectedItemImage_Animator;

    void Start ()
    {
        // inventoryItems.AddRange( GetComponentsInChildren<InventoryItem>() );
        inventoryItems.AddRange( GetComponents<InventoryItem>() );
        PairInventoryItemsToInputKeys();
        selectedItem_Image = SelectedItemImage_GO.GetComponent<Image>();
        selectedItemImage_Animator =  SelectedItemImage_GO.GetComponent<Animator>();

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

        if( (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonUp(1))  /* && inventoryKeyLoock == false */)
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
            selectedItem_Image.sprite = selectedInventoryItem.ItemIcon;
            selectedItemImage_Animator.SetTrigger("SetNewItem");
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
