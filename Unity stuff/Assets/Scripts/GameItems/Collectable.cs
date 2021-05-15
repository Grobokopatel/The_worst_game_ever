using System;
using UnityEngine;

public class Collectable : InteractableObject
{
    public override void Interact(Player player)
    {
        var resource = Technical.GetItem(gameObject.name.GetItemNameWithoutAdditInfo());
        player.AddDeltaItems(resource, 1);
        Destroy(gameObject);
        Debug.Log($"Количество {resource} в инвентаре: {player.GetAmountOfItem(resource)}");
    }
}