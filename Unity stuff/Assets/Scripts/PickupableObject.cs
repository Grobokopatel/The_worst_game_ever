using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : InteractableObject
{
    public InGameResources resource;
    public override void Interact(Player player)
    {
        player.AddDeltaResources(resource, 1);
        Destroy(gameObject);
        Debug.Log($"Количество брёвен в инвентаре: {player.GetAmountOfResource(InGameResources.Log)}");
    }
}

public class Log1 : PickupableObject
{
    private void Awake()
    {
        resource = InGameResources.Log;
    }
}

public class Stone : PickupableObject
{
    private void Awake()
    {
        resource = InGameResources.Stone;
    }
}