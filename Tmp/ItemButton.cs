using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Craft item;
    public Image image;
    public Text name;
    public Transform subHolder;

    public void Craft()
    {
        var inv = FindObjectOfType<Inventory>();
        for (int i = 0; i < item.items.Count; i++)
        {
            if (inv.GetItem(item.items[i].name) != null)
            {
                if (inv.GetItem(item.items[i].name).val < item.items[i].val) return;
            }
            else
                return;
        }
        
        for (int i = 0; i < item.items.Count; i++)
        {
            inv.GetItem(item.items[i].name).val -= item.items[i].val;
        }
        inv.AddItem(GlobalItemsList.GetItem(item.itemName).Clone());
    }
}
