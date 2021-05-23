using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if (CraftingRecipe.CanCraft())
        {
            Debug.Log($"�� �������� {CraftingRecipe.Results[0].Item.ItemName}");
            CraftingRecipe.Craft();
            Debug.Log($"���������� ��� ����� ������ {player.GetAmountOfItem("Saw")}");
            Debug.Log($"���������� ������ ����� ������ {player.GetAmountOfItem("Rock")}");
            CraftingMenu.craftingMenu.UpdateItemsAmount();
            GetComponent<Button>().interactable = false;
            Destroy(gameObject, 0.05F);
        }
        else
        {
            Debug.Log("������������ ��������");
        }
    }
}
