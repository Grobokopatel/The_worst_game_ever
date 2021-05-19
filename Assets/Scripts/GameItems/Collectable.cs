using System;
using UnityEngine;

public class Collectable : Interactable
{
    public override void Interact(Player player)
    {
        var resource = Technical.GetItem(gameObject.name.GetItemNameWithoutAdditInfo());
        player.AddDeltaItems(resource, 1);
        Destroy(gameObject);
        Destroy(gameObject);
        Debug.Log($"���������� {resource} � ���������: {player.GetAmountOfItem(resource)}");
    }
}