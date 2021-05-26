using UnityEngine;

public class Collectable : Interactable
{
    public override void Interact()
    {
        var resource = Technical.GetItem(gameObject.name.GetItemNameWithoutAdditInfo());
        Player.player.AddDeltaItems(resource, 1);
        Destroy(gameObject);
        Player.CreateText(resource.ItemName);
        Debug.Log($"Количество {resource} в инвентаре: {Player.player.GetAmountOfItem(resource)}");
    }
}