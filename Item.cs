using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string equipmentType;
    public int equipmentIndex;

    public Sprite itemSprite;

    public int amountInStack = 1;

    public int maxStackSize = 1000;

    public int itemID;
}
