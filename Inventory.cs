using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryObject;

    public Slot[] slots;

    public Slot[] equipSlots;
    private void Start()
    {
        foreach (Slot i in slots)
        {
            i.CustomStart();
        }
        foreach(Slot i in equipSlots)
        {
            i.CustomStart();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryObject.SetActive(!inventoryObject.activeInHierarchy);
        }

        foreach (Slot i in slots)
            i.CheckForItem();
        foreach (Slot i in equipSlots)
            i.CheckForItem();

    }

    public int GetItemAmount(int id)
    {
        int num = 0;
        foreach (Slot i in slots)
        {
            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if (z.itemID == id)
                    num += z.amountInStack;
            }
        }
        return num;
    }

    public void RemoveItemAmount(int id, int amountToRemove)
    {
        foreach(Slot i in slots)
        {
            if (amountToRemove <= 0)
                return;

            if(i.slotsItem)
            {
                Item z = i.slotsItem;
                if(z.itemID == id)
                {
                    int amountThatCanBeRemoved = z.amountInStack;
                    if(amountThatCanBeRemoved <= amountToRemove)
                    {
                        Destroy(z.gameObject);
                        amountToRemove -= amountThatCanBeRemoved;
                    }
                    else
                    {
                        z.amountInStack -= amountToRemove;
                        amountToRemove = 0;
                    }
                }
            }
        }
    }
    public void AddItem(Item itemToBeAdded, Item startingItem = null)
    {
        int amountInStack = itemToBeAdded.amountInStack;
        List<Item> stackableItems = new List<Item>();
        List<Slot> emptySlots = new List<Slot>();

        if(startingItem && startingItem.itemID == itemToBeAdded.itemID && startingItem.amountInStack < startingItem.maxStackSize)
            stackableItems.Add(startingItem);

        foreach (Slot i in slots)
        {
            if (i.slotsItem)
            {
                Item z = i.slotsItem;
                if (z.itemID == itemToBeAdded.itemID && z.amountInStack < z.maxStackSize && z != startingItem)
                    stackableItems.Add(z);
            }
            else
            {
                emptySlots.Add(i);
            }
        }

        foreach(Item i in stackableItems)
        {
            int amountThatCanBeAdded = i.maxStackSize - i.amountInStack;
            if(amountInStack <= amountThatCanBeAdded)
            {
                i.amountInStack += amountInStack;
                Destroy(itemToBeAdded.gameObject);
                return;
            }
            else
            {
                i.amountInStack = i.maxStackSize;
                amountInStack -= amountThatCanBeAdded;
            }
        }

        itemToBeAdded.amountInStack = amountInStack;
        if(emptySlots.Count > 0)
        {
            itemToBeAdded.transform.parent = emptySlots[0].transform;
            itemToBeAdded.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Item>())
            AddItem(col.GetComponent<Item>());
    }
}
