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
            Debug.Log($"�� �������� {ThisButtonRecipe.Results[0].Item.ItemName}");
            ThisButtonRecipe.Craft(player);
            Debug.Log($"���������� ����� ����� ������ {player.GetAmountOfItem(Resources.Load<Item>("Inventory Items/Log"))}");
            Debug.Log($"���������� ������ ����� ������ {player.GetAmountOfItem(Resources.Load<Item>("Inventory Items/Stone"))}");
            CraftingMenu.craftingMenu.UpdateItemsAmount();
        }
        else
        {
            Debug.Log("������������ ��������");
        }
    }
}
