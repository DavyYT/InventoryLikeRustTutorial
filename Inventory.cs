using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryObject;

    public Slot[] slots;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
        }
    }

    public void AddItem(Item itemToBeAdded, Item startingItem = null)
    {
        foreach(Slot i in slots)
        {
            if(!i.slotsItem)
            {
                itemToBeAdded.transform.parent = i.transform;
                return;
            }
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Item>())
            AddItem(col.GetComponent<Item>());
    }
}
