using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CirclesOnWater : MonoBehaviour
{
    public static bool isCasualDifficultyOn;
    [SerializeField]
    private List<ItemAmount> storedItems;
    [SerializeField]
    private float secondsPerArrow;

    [SerializeField]
    private int minArrowAmount;
    public int MinArrowAmount
    {
        get => minArrowAmount;
    }

    [SerializeField]
    private int maxArrowAmount;
    public int MaxArrowAmount
    {
        get => minArrowAmount;
    }

    public float SecondsPerArrow
    {
        get => isCasualDifficultyOn ? 1 : secondsPerArrow;
    }

    public List<ItemAmount> StoredItems
    {
        get => storedItems;
    }

    private void Awake()
    {
        if (secondsPerArrow == 0)
            secondsPerArrow = 0.35F;
    }

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
