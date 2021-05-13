using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private SpriteRenderer sprite;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player)
        {
            if (player.CurrentInteractable != null)
                player.CurrentInteractable.sprite.color = Color.white;
            sprite.color = Color.yellow;
            player.CurrentInteractable = this;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && player.CurrentInteractable == null)
        {
            sprite.color = Color.yellow;
            player.CurrentInteractable = this;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && player.CurrentInteractable == this)
        {
            sprite.color = Color.white;
            player.CurrentInteractable = null;
        }
    }

    public abstract void Interact(Player player);

    public virtual void Awake()
    {
        enabled = false;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
}
