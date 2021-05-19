using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradingMenu : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
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
            //Debug.Log(material.Item1.text.Split('/')[2]);
            var needed = int.Parse(material.Item1.text.Split(new[] { '/', '<', '>' }, StringSplitOptions.RemoveEmptyEntries)[2]);
            var color = playerHas < needed ? "red" : "white";
            material.Item1.text = $"<color={color}>{playerHas}/{needed}</color>";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            Player.player.enabled = true;
            cameraController.XOffset = 0;
        }
    }

    private void Awake()
    {
        
        gameObject.SetActive(false);
        tradingMenu = this;
        var tradesInfo = Resources.LoadAll<CraftingRecipe>("Prefabs/Trades info");
        foreach (var tradeInfo in tradesInfo)
        {
            var tradeObject = Instantiate(tradePrefab, tradeHolder.transform);
            var firstResult = tradeInfo.Results[0];
            var firstMaterial = tradeInfo.Materials[0];
            var tradeButton = tradeObject.GetComponent<TradeButton>();

            tradeButton.TradeInfo = tradeInfo;
            var materialObject = tradeButton.MaterialObject;
            var resultObject = tradeButton.ResultObject;

            materialObject.GetComponentInChildren<Image>().sprite = firstMaterial.Item.Icon;
            materialObject.GetComponentInChildren<Text>().text = $"{Player.player.GetAmountOfItem(firstMaterial.Item)}/{firstMaterial.Amount}";

            resultObject.GetComponentInChildren<Image>().sprite = firstResult.Item.Icon;
            resultObject.GetComponentInChildren<Text>().text = firstResult.Amount.ToString();

            tradeButton.NameObject.text = firstResult.Item.ItemName;
        }
        Debug.Log("Инициализация меню трейдинга");
    }
}
