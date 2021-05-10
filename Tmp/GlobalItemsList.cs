using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalItemsList : MonoBehaviour
{
    public static GlobalItemsList gl;
    public List<Item> allItemsList;
    private void Start()
    {
        gl = this;
    }
    public static Item GetItem(string name)
    {
        return GlobalItemsList.gl.allItemsList.Find(x => x.name == name);
    }
}
