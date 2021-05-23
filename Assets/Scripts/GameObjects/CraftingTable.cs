using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : Interactable
{
    [SerializeField]
    private CraftingMenu craftingMenu;
    [SerializeField]
    private CameraController cameraController;

    public override void Interact()
    {
        if (Player.player.GetAmountOfItem("ShovelRecipe") >= 1)
        {
            Player.player.AddDeltaItems("ShovelRecipe", -1);
            CraftingMenu.craftingMenu.AddRecipeOnCanvas(Resources.Load<CraftingRecipe>("Prefabs/Crafting recipes/ShovelRecipe"));
        }

        StartCoroutine(Technical.WaitThenInvokeMethod(0, () =>
        {
            Player.player.State = PlayerState.Idle;
            Player.player.enabled = false;
        }));
        
        craftingMenu.gameObject.SetActive(true);
        craftingMenu.UpdateItemsAmount();
        cameraController.XOffset = 3.5F;
    }
}
