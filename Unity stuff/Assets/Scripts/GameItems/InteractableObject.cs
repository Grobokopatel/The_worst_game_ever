using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public SpriteRenderer Sprite
    {
        get;
        set;
    }
    
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && ShouldHighlight(player))
        {
            if (player.CurrentInteractable != null)
                player.CurrentInteractable.Sprite.color = Color.white;
            Sprite.color = Color.yellow;
            player.CurrentInteractable = this;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && ShouldHighlight(player) && player.CurrentInteractable == null)
        {
            Sprite.color = Color.yellow;
            player.CurrentInteractable = this;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();

        if (player && player.CurrentInteractable == this)
        {
            Sprite.color = Color.white;
            player.CurrentInteractable = null;
        }
    }

    public abstract void Interact(Player player);

    public virtual bool ShouldHighlight(Player player)
    {
        return true;
    }

    public virtual void Awake()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
    }
}
