using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    protected Color defaultColor = Color.white;
    protected Color highlightedColor = new Color(0.7F, 0.64F, 0);
    public SpriteRenderer Sprite
    {
        get;
        set;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player)
        {
            OnPlayerEntry(player);
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player)
        {
            OnPlayerEntry(player);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player)
        {
            OnPlayerExit(player);
        }
    }

    protected event Action<Player> OnPlayerEntry;
    protected event Action<Player> OnPlayerExit;


    protected void Awake()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
        OnPlayerEntry += player =>
        {
            Sprite.color = highlightedColor;
            player.Boat = this;
        };

        OnPlayerExit += player =>
        {
            Sprite.color = defaultColor;
            player.Boat = null;
        };
    }
}
