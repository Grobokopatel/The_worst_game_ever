using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Color defaultColor = Color.white;
    protected Color highlightedColor = Color.yellow;
    public SpriteRenderer Sprite
    {
        get;
        set;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player && ShouldHighlight(player))
        {
            if (player.CurrentInteractable != null)
                player.CurrentInteractable.OnPlayerExit(player);
            OnPlayerEntry(player);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && ShouldHighlight(player) && player.CurrentInteractable == null)
            OnPlayerEntry(player);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && ShouldHighlight(player) && player.CurrentInteractable == this)
            OnPlayerExit(player);
    }
    
    public abstract void Interact(Player player);

    protected event Action<Player> OnPlayerEntry;
    protected event Action<Player> OnPlayerExit;

    protected virtual bool ShouldHighlight(Player player)
    {
        return true;
    }

    protected virtual void Awake()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
        OnPlayerEntry += player =>
        {
            Sprite.color = highlightedColor;
            player.CurrentInteractable = this;
        };

        OnPlayerExit += player =>
        {
            Sprite.color = defaultColor;
            player.CurrentInteractable = null;
        };
    }
}
