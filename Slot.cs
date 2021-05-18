using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Item slotsItem;

    Sprite defaultSprite;
    Text amountText;

    private void Start()
    {
        defaultSprite = GetComponent<Image>().sprite;
        amountText = transform.GetChild(0).GetComponent<Text>();
        amountText.text = "";
    }

    private void Update()
    {
        CheckForItem();
    }

    private void CheckForItem()
    {
        if(transform.childCount > 1)
        {
            slotsItem = transform.GetChild(1).GetComponent<Item>();
            GetComponent<Image>().sprite = slotsItem.itemSprite;
            if (slotsItem.amountInStack > 1)
                amountText.text = slotsItem.amountInStack.ToString();
        }
        else
        {
            slotsItem = null;
            GetComponent<Image>().sprite = defaultSprite;
            amountText.text = "";
        }
    }
}
