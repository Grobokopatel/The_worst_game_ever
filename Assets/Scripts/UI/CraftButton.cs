using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public CraftingRecipe CraftingRecipe
    {
        get;
        set;
    }    

    public void OnClick()
    {
        var player = Player.player;
        if (CraftingRecipe.CanCraft(player))
        {
            Debug.Log($"�� �������� {CraftingRecipe.Results[0].Item.ItemName}");
            CraftingRecipe.Craft(player);
            Debug.Log($"���������� ����� ����� ������ {player.GetAmountOfItem("Log")}");
            Debug.Log($"���������� ������ ����� ������ {player.GetAmountOfItem("Rock")}");
            CraftingMenu.craftingMenu.UpdateItemsAmount();
        }
        else
        {
            Debug.Log("������������ ��������");
        }
    }
}
