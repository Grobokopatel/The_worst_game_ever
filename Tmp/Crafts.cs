using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Craft {
    public string itemName;
    public List<Item> items;
}

public class Crafts : MonoBehaviour
{
    public static Crafts cr;
    public List<Craft> crafts;

    private void Start()
    {
        cr = this;
    }
}
