using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Linq;

public class CraftingMenu : ExchangeMenu
{
    [SerializeField]
    private GameObject craftPrefab;

    [SerializeField]
    private GameObject materialPrefab;

    [SerializeField]
    private GameObject craftHolder;

    private readonly List<(Text, Item)> allMaterials = new List<(Text, Item)>();

    public static CraftingMenu craftingMenu;

    public void UpdateItemsAmount()
    {
        foreach (var material in allMaterials)
        {
            if (material.Item1 != null)
            {
                var playerHas = Player.player.GetAmountOfItem(material.Item2);
                var needed = int.Parse(material.Item1.text.Split(new[] { '/', '<', '>' }, StringSplitOptions.RemoveEmptyEntries)[2]);
                var color = playerHas < needed ? "red" : "white";
                material.Item1.text = $"<color={color}>{playerHas}/{needed}</color>";
            }
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
        craftingMenu = this;
        var craftingRecipes = Resources.LoadAll<CraftingRecipe>("Prefabs/Crafting recipes")
            .Where(recipe => !recipe.ShouldNotLoadDuringInitialization)
            .OrderBy(recipe => recipe.SortOrder);
        foreach (var craftingRecipe in craftingRecipes)
        {
            AddRecipeOnCanvas(craftingRecipe);
        }
        Debug.Log("Инициализация меню крафта");
    }

    public void AddRecipeOnCanvas(CraftingRecipe craftingRecipe)
    {
        var craft = Instantiate(craftPrefab, craftHolder.transform);
        var firstResult = craftingRecipe.Results[0].Item;
        craft.GetComponentsInChildren<Image>(true)[1].sprite = firstResult.Icon;
        craft.GetComponentInChildren<Text>(true).text = firstResult.ItemName;
        craft.GetComponent<CraftButton>().CraftingRecipe = craftingRecipe;

        foreach (var craftingRecipeMaterial in craftingRecipe.Materials)
        {
            var material = Instantiate(materialPrefab, craft.GetComponentsInChildren<RectTransform>(true)[3].transform);
            allMaterials.Add((material.GetComponentInChildren<Text>(true), craftingRecipeMaterial.Item));

            material.GetComponentInChildren<Image>(true).sprite = craftingRecipeMaterial.Item.Icon;
            material.GetComponentInChildren<Text>(true).text = $"<color=white>0/{craftingRecipeMaterial.Amount}</color>";
        }
    }
}
