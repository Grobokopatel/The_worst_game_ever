using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
    [SerializeField]
    private CraftingMenu craftingMenu;
    [SerializeField]
    private CameraController cameraController;

    public override void Interact(Player player)
    {
        if(Player.player.GetAmountOfItem("ShovelRecipe")>=1)
        {
            Player.player.AddDeltaItems("ShoverRecipe", -1);

        }

        craftingMenu.gameObject.SetActive(true);
        craftingMenu.UpdateItemsAmount();
        Player.player.enabled = false;
        cameraController.XOffset = 3.5F;
    }
}
