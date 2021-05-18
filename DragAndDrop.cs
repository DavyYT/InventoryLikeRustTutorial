using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Inventory inv;

    GameObject curSlot;
    Item curSlotsItem;

    public Image followMouseImage;

    private void Update()
    {
        followMouseImage.transform.position = Input.mousePosition;

        if(Input.GetKeyDown(KeyCode.G))
        {
            GameObject obj = GetObjectUnderMouse();
            if (obj)
                obj.GetComponent<Slot>().DropItem();
        }

        if (Input.GetMouseButtonDown(0))
        {
            curSlot = GetObjectUnderMouse();
        }
        else if (Input.GetMouseButton(0))
        {
            if (curSlot && curSlot.GetComponent<Slot>().slotsItem)
            {
                followMouseImage.color = new Color(255, 255, 255, 255);
                followMouseImage.sprite = curSlot.GetComponent<Image>().sprite;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (curSlot && curSlot.GetComponent<Slot>().slotsItem)
            {
                curSlotsItem = curSlot.GetComponent<Slot>().slotsItem;
                GameObject newObj = GetObjectUnderMouse();
                if (newObj && newObj != curSlot)
                {
                    if (newObj.GetComponent<EquipmentSlot>() && newObj.GetComponent<EquipmentSlot>().equipmentType != curSlotsItem.equipmentType)
                        return;

                    if (newObj.GetComponent<Slot>().slotsItem)
                    {
                        Item objectsItem = newObj.GetComponent<Slot>().slotsItem;
                        if (objectsItem.itemID == curSlotsItem.itemID && objectsItem.amountInStack != objectsItem.maxStackSize && !newObj.GetComponent<EquipmentSlot>())
                        {
                            inv.AddItem(curSlotsItem, objectsItem);
                        }
                        else
                        {
                            objectsItem.transform.parent = curSlot.transform;
                            curSlotsItem.transform.parent = newObj.transform;
                        }
                    }
                    else
                    {
                        curSlotsItem.transform.parent = newObj.transform;
                    }
                }
            }
            foreach(Slot i in inv.equipSlots)
            {
                i.GetComponent<EquipmentSlot>().Equip();
            }
        }
        else
        {
            followMouseImage.sprite = null;
            followMouseImage.color = new Color(0, 0, 0, 0);
        }
    }

    GameObject GetObjectUnderMouse()
    {
        GraphicRaycaster raycaster = GetComponent<GraphicRaycaster>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        eventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        raycaster.Raycast(eventData, results);

        foreach(RaycastResult i in results)
        {
            if (i.gameObject.GetComponent<Slot>())
                return i.gameObject;
        }
        return null;
    }
}
