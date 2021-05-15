using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Technical
{
    public static IEnumerator Timer(float seconds, Action whatToDoAfterDelay)
    {
        yield return new WaitForSeconds(seconds);
        whatToDoAfterDelay();
    }

    public static Item GetItem(this string itemName)
    {
        return Resources.Load<Item>($"Prefabs/Inventory items/{itemName}");
    }

    public static string GetItemNameWithoutAdditInfo(this string name)
    {
        return name.Split(new[] { ' ', '(' })[0];
    }
}
