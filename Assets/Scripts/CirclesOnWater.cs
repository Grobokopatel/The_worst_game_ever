using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CirclesOnWater : MonoBehaviour
{
    public List<ItemAmount> storedItems;

    public Item GetRandomItem()
    {
        var count = storedItems.Count;

        var index = new System.Random().Next(0, count);
        var choosedItemAmount = storedItems[index];
        var returnedItem = choosedItemAmount.Item;
        var amount = choosedItemAmount.Amount;
        if (amount == 1)
        {
            storedItems.Remove(choosedItemAmount);
            if (count == 1)
                Destroy(gameObject);
        }
        else
        {
            choosedItemAmount.Amount = amount - 1;
            storedItems[index] = choosedItemAmount;
        }

        return returnedItem;
    }
}
