using System;
using UnityEngine;

public class Collectable : InteractableObject
{
    public override void Interact(Player player)
    {
        var resource = Resources.Load<Item>($"Inventory Items/{gameObject.name.Split(new[] { ' ', '(' })[0]}");
        player.AddDeltaItems(resource, 1);
        Destroy(gameObject);
        Debug.Log($"Количество {resource} в инвентаре: {player.GetAmountOfItem(resource)}");
    }
}