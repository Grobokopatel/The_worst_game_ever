using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeButton : MonoBehaviour
{
    [SerializeField]
    private GameObject materialObject;
    public GameObject MaterialObject
    {
        get => materialObject;
    }

    [SerializeField]
    private GameObject resultObject;
    public GameObject ResultObject
    {
        get => resultObject;
    }

    [SerializeField]
    private Text nameObject;
    public Text NameObject
    {
        get => nameObject;
    }

    public CraftingRecipe TradeInfo
    {
        get;
        set;
    }

    public void OnClick()
    {
        var player = Player.player;
        if (TradeInfo.CanCraft())
        {
            Debug.Log($"Ты скрафтил {TradeInfo.Results[0].Item.ItemName}");
            TradeInfo.Craft();
            Debug.Log($"Количество брёвен после крафта {player.GetAmountOfItem("Log")}");
            Debug.Log($"Количество камней после крафта {player.GetAmountOfItem("Rock")}");
            TradingMenu.tradingMenu.UpdateItemsAmount();
        }
        else
        {
            Debug.Log("Недостаточно ресурсов");
        }
    }
}
