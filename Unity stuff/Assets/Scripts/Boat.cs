using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : InteractableObject
{
    public override void Interact(Player player)
    {
        player.transform.position += (-1.5F) * transform.up;
        Destroy(gameObject);
        player.State = PlayerState.InBoat;
    }
}
