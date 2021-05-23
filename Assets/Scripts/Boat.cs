using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Interactable
{
    public override void Interact()
    {
        Player.player.transform.position = transform.position;
        Destroy(gameObject);
        Player.player.State = PlayerState.InBoat;
    }
}
