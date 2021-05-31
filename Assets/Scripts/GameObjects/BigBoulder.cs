using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoulder : Interactable
{
    [SerializeField]
    private AudioClip soundToProduce;
    public override void Interact()
    {
        if (Player.player.GetAmountOfItem("Dynamite") >= 1)
        {
            Player.player.AddDeltaItems("Dynamite", -1);
            AudioManager.PlayAudio(soundToProduce);
            Destroy(gameObject);
        }
        else
        {
            PopUpTextCreator.QueueText("Мне бы чем-нибудь взорвать этот камень");
        }
    }
}
