using UnityEngine;

public class Collectable : Interactable
{
    public override void Interact()
    {
        var resource = gameObject.name.GetItemNameWithoutAdditInfo().GetItemWithThisName();
        Player.player.AddDeltaItems(resource, 1);
        Destroy(gameObject);
        Debug.Log($"Количество {resource} в инвентаре: {Player.player.GetAmountOfItem(resource)}");
        AudioManager.PlayAudio(AudioManager.CollectSound);
    }
}