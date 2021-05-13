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
    public int Amount => amount;
}



[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(Player player)
    {
        return Materials.All(material => player.GetAmountOfItem(material.Item) >= material.Amount);
    }

    public void Craft(Player player)
    {
        foreach(var material in Materials)
            player.AddDeltaItems(material.Item, -material.Amount);

        foreach (var result in Results)
            player.AddDeltaItems(result.Item, result.Amount);
    }
}
