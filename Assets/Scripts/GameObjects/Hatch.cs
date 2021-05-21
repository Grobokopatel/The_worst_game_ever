using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : Interactable
{
    public bool isDugOut = false;

    public override void Interact(Player player)
    {
        if(player.GetAmountOfItem("Key")>=1)
        {
            Debug.Log("Конец игры");
        }
    }

    protected override bool ShouldHighlight(Player player)
    {
        return isDugOut;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)
            && Player.player.GetAmountOfItem("Shovel") >= 1
            && Mathf.Abs(transform.position.x - Player.player.transform.position.x) <= 1)
        {
            isDugOut = true;
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }
    }
}
