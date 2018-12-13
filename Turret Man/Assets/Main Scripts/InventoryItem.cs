using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class InventoryItem : MonoBehaviour, IComparable<InventoryItem> {

    /// <summary>
    /// Lock the button/key so you can just spam the item. You must wait for the Item Animation or whatever before you can use it again
    /// </summary>
    public bool itemSpamKeyLock;

    [SerializeField] protected int inputKeyPadID;
    [SerializeField] protected string itemName;



    public string ItemName
    {
        get
        {
            return itemName;
        }

        set
        {
            itemName = value;
        }
    }

    public int KeyId
    {
        get
        {
            return inputKeyPadID;
        }

        set
        {
            inputKeyPadID = value;
        }
    }


    public int CompareTo(InventoryItem other)
    {
        //return this.KeyId.CompareTo(other.KeyId);

        if(KeyId > other.KeyId)
        {
            return 1;
        }
        else if(KeyId < other.KeyId)
        {
            return -1;
        }else
        {
            // TODO If 2 items have the same KeyID i need to cancel the Change Or handle it in some other way.
            Debug.LogError(ItemName + " | AND | " + other.ItemName + " | Have same KeyID -- > Keys are now Broken may crash");
            return 0;
        }
    }

    public abstract void Action();
}
