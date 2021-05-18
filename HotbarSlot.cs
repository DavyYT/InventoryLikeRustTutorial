using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSlot : MonoBehaviour
{
    public GameObject[] unstaticItems;

    public static GameObject[] items;

    public KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        if (unstaticItems.Length > 0)
            items = unstaticItems;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
            Equip();
    }

    void Equip()
    {
        if(transform.childCount > 1)
        {
            Item item = transform.GetChild(1).GetComponent<Item>();

            if(item.equipmentType == "Hand")
            {
                for(int i = 0; i < items.Length; i++)
                {
                    if(i == item.equipmentIndex)
                    {
                        items[i].SetActive(!items[i].activeInHierarchy);
                    }
                    else
                    {
                        items[i].SetActive(false);
                    }

                }
            }
        }
    }
}
