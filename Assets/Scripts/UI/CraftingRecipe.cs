using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[Serializable]
public struct ItemAmount
{
    [SerializeField]
    private Item item;
    public Item Item => item;

    [SerializeField]
    [Range(0,999)]
    private int amount;
    public int Amount
    {
        get => amount;
        set
        {
            amount = value;
        }
    }
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    private int sortOrder;
    public int SortOrder
    {
        get => sortOrder;
    }

    [SerializeField]
    private bool shouldNotLoadDuringInitialization;
    public bool ShouldNotLoadDuringInitialization
    {
        get => shouldNotLoadDuringInitialization;
    }

    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft()
    {
        return Materials.All(material => Player.player.GetAmountOfItem(material.Item) >= material.Amount);
    }

    public void Craft()
    {
        foreach(var material in Materials)
            Player.player.AddDeltaItems(material.Item, -material.Amount);

        foreach (var result in Results)
            Player.player.AddDeltaItems(result.Item, result.Amount);
    }
}
