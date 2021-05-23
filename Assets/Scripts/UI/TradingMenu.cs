using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TradingMenu : ExchangeMenu
{
    [SerializeField]
    private GameObject tradePrefab;
    [SerializeField]
    private GameObject tradeHolder;

    private readonly List<(Text, Item)> allMaterials = new List<(Text, Item)>();

    public static TradingMenu tradingMenu;

    public void UpdateItemsAmount()
    {
        foreach (var material in allMaterials)
        {
            var playerHas = Player.player.GetAmountOfItem(material.Item2);
            var needed = int.Parse(material.Item1.text.Split(new[] { '/', '<', '>' }, StringSplitOptions.RemoveEmptyEntries)[2]);
            var color = playerHas < needed ? "red" : "white";
            material.Item1.text = $"<color={color}>{playerHas}/{needed}</color>";
        }
    }


    private void Awake()
    {
        gameObject.SetActive(false);
        tradingMenu = this;
        var tradesInfo = Resources.LoadAll<CraftingRecipe>("Prefabs/Trades info").OrderBy(recipe => recipe.SortOrder);
        foreach (var tradeInfo in tradesInfo)
        {
            var tradeObject = Instantiate(tradePrefab, tradeHolder.transform);
            var firstResult = tradeInfo.Results[0];
            var firstMaterial = tradeInfo.Materials[0];
            var buttonComponent = tradeObject.GetComponent<TradeButton>();

            buttonComponent.TradeInfo = tradeInfo;
            var materialObject = buttonComponent.MaterialObject;
            var resultObject = buttonComponent.ResultObject;

            materialObject.GetComponentInChildren<Image>().sprite = firstMaterial.Item.Icon;
            materialObject.GetComponentInChildren<Text>().text = $"<color=white>{Player.player.GetAmountOfItem(firstMaterial.Item)}/{firstMaterial.Amount}</color>";

            resultObject.GetComponentInChildren<Image>().sprite = firstResult.Item.Icon;
            resultObject.GetComponentInChildren<Text>().text = firstResult.Amount.ToString();

            buttonComponent.NameObject.text = firstResult.Item.ItemName;

            allMaterials.Add((materialObject.GetComponentInChildren<Text>(), firstMaterial.Item));
        }
        Debug.Log("Инициализация меню трейдинга");
    }
}
