using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Interactable
{
    public override void Interact()
    {
        Player.player.State = PlayerState.InBoat;
        Player.player.transform.position = transform.position;
        Destroy(gameObject);
    }
}
