using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Log : InteractableObject
{
    public override void Interact(Player player)
    {
        player.AddDeltaResources(InGameResources.Log, 1);
        Destroy(gameObject);
        Debug.Log($"Количество брёвен в инвентаре: {player.GetAmountOfResource(InGameResources.Log)}");
    }
}