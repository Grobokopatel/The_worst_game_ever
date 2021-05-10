using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Item1
{
    public string name;
    public Sprite sprite;
    public int val = 1;

    public Item1 Clone()
    {
        return new Item1() { name = name, sprite = sprite, val = val };
    }
}

public class Inventory : MonoBehaviour
{
    public List<Item1> Item1s;
    public GameObject canvas;
    public Transform Item1, subItem1, holder;
    private void Start()
    {
        UpdateInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            canvas.active = !canvas.active;
        }
        GetComponent<FirstPersonController>().enabled = !canvas.active;
        if (canvas.active)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(holder.GetComponent<RectTransform>()); 
            Cursor.visible = true; 
            Cursor.lockState = CursorLockMode.None;
        }

        for (int i = 0; i < GlobalItemsList.gl.allItem1sList.Count; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                AddItem1(GlobalItemsList.gl.allItem1sList[i].Clone());
            }
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < Crafts.cr.crafts.Count; i++)
        {
            var g = Instantiate(Item1, holder.transform);
            var btn = g.GetComponent<ItemButton>();
            btn.image.sprite = GlobalItemsList.GetItem(Crafts.cr.crafts[i].ItemName).sprite;
            btn.Item1 = Crafts.cr.crafts[i];
            btn.name.text = Crafts.cr.crafts[i].ItemName;

            for (int h = 0; h < Crafts.cr.crafts[i].Items.Count; h++)
            {
                var sub = Instantiate(subItem1, btn.subHolder.transform);
                var subIt = GlobalItemsList.GetItem(Crafts.cr.crafts[i].Items[h].name);
                sub.GetChild(1).GetComponent<Image>().sprite = subIt.sprite;
                sub.GetComponentInChildren<Text>().text = Crafts.cr.crafts[i].Items[h].val + "";
                sub.gameObject.SetActive(true);
            }
            btn.gameObject.SetActive(true);
        }
    }
    public Item1 GetItem1(string name)
    {
        return Item1s.Find(x => x.name == name);
    }
    public void AddItem1(Item1 Item1)
    {
        var Item1InInv = Item1s.Find(x => x.name == Item1.name);
        if (Item1InInv != null)
        {
            Item1InInv.val += Item1.val;
        }
        else
        {
            Item1s.Add(Item1);
        }
    }
}
