using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public CraftingRecipe ThisButtonRecipe
    {
        get;
        set;
    }    

    public void OnClick()
    {
        var player = Player.player;
        if (ThisButtonRecipe.CanCraft(player))
        {
            Debug.Log($"Ты скрафтил {ThisButtonRecipe.Results[0].Item.ItemName}");
            ThisButtonRecipe.Craft(player);
            Debug.Log($"Количество брёвен после крафта {player.GetAmountOfItem("Log")}");
            Debug.Log($"Количество камней после крафта {player.GetAmountOfItem("Rock")}");
            CraftingMenu.craftingMenu.UpdateItemsAmount();
        }
        else
        {
            Debug.Log("Недостаточно ресурсов");
        }
    }
}
