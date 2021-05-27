using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bridge : Interactable
{
    [SerializeField]
    private GameObject materials;
    [SerializeField]
    private Image mark;
    [SerializeField]
    private Text amount;
    [SerializeField]
    private Sprite checkMark;
    [SerializeField]
    private Sprite cross;
    private BoxCollider2D collider;
    private bool isRepaired = false;

    protected override void Awake()
    {
        base.Awake();
        OnPlayerEntry += player =>
        {
            materials.SetActive(true);
            UpdateItemsAmount();
        };
        OnPlayerExit += player => materials.SetActive(false);
        defaultColor = new Color(255, 255, 255, 0);
        highlightedColor = new Color(0, 255, 1, 0.6588F);
        collider = GetComponent<BoxCollider2D>();
    }

    private void UpdateItemsAmount()
    {
        var playerHas = Player.player.GetAmountOfItem("Log");
        var color = playerHas < 3 ? "red" : "white";
        amount.text = $"<color={color}>{playerHas}/3</color>";
        mark.sprite = Player.player.GetAmountOfItem("Axe") >= 1 ? checkMark : cross;
    }

    protected override bool ShouldHighlight()
    {
        return !isRepaired;
    }

    public override void Interact()
    {
        if (Player.player.GetAmountOfItem("Log") >= 3 && Player.player.GetAmountOfItem("Axe") >= 1)
        {
            Player.player.AddDeltaItems("Log", -3);

            materials.SetActive(false);
            Sprite.color = new Color(255, 255, 255, 1);
            defaultColor = new Color(255, 255, 255, 1);
            enabled = false;
            isRepaired = true;

            var oldSize = collider.size;
            oldSize.y = 3;
            collider.size = oldSize;

        }
        else
        {
            PopUpTextCreator.TextsToPopUp.Enqueue(($"Мне не хватает ресурсов для починки", Color.red));
        }
    }
}


