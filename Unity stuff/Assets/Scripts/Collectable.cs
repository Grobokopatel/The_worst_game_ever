using System;
using UnityEngine;

public class Collectable : InteractableObject
{
    public override void Interact(Player player)
    {
        var resource =
            (InGameResources)Enum.Parse(typeof(InGameResources), gameObject.name.Replace("(Clone)", "").Split()[0]);
        player.AddDeltaResources(resource, 1);
        Destroy(gameObject);
        Debug.Log($"Количество {resource.ToString()} в инвентаре: {player.GetAmountOfResource(resource)}");
    }
}