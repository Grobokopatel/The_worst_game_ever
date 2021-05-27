using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoulder : Interactable
{
public override void Interact()
    {
        if(Player.player.GetAmountOfItem("Dynamite")>=1)
        {
            Player.player.AddDeltaItems("Dynamite", -1);
            Destroy(gameObject);
        }
        else
        {
            PopUpTextCreator.TextsToPopUp.Enqueue(("��� �� ���-������ �������� ���� ������", Color.white));
        }
    }
}
