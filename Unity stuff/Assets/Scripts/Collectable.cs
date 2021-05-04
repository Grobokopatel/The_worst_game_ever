using System;
using UnityEngine;

public class Collectable : InteractableObject
{
    public override void Interact(Player player)
    {
        var resource = (InGameResources)Enum.Parse(typeof(InGameResources), gameObject.name.Split(new[] {' ', '(' })[0]);
        player.AddDeltaResources(resource, 1);
        Destroy(gameObject);
        Debug.Log($"Количество {resource} в инвентаре: {player.GetAmountOfResource(resource)}");
    }
}