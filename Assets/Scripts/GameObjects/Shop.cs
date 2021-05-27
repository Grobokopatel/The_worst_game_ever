using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    [SerializeField]
    private TradingMenu tradingMenu;
    [SerializeField]
    private CameraController cameraController;
    public static bool WasCanvasOpenedBefore = false;

    public override void Interact()
    {
        StartCoroutine(Technical.WaitThenInvokeMethod(0, () =>
        {
            Player.player.State = PlayerState.Idle;
            Player.player.enabled = false;
        }));

        tradingMenu.gameObject.SetActive(true);
        tradingMenu.UpdateItemsAmount();
        cameraController.XOffset = 3F;

        if (!WasCanvasOpenedBefore)
        {
            WasCanvasOpenedBefore = true;
            CraftingMenu.craftingMenu.AddRecipeOnCanvas(Resources.Load<CraftingRecipe>("Prefabs/Crafting recipes/KeyRecipe"));
        }
    }
}
