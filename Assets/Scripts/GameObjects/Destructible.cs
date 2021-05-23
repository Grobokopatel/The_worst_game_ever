using System.Collections.Generic;
using UnityEngine;

public class Destructible : Interactable
{
    [SerializeField]
    private GameObject objectToSpawn;
    [SerializeField]
    private Item itemToDestroyThis;

    public override void Interact()
    {
        var objectTransform = gameObject.transform;
        Destroy(gameObject);
        if (objectToSpawn != null)
            Instantiate(objectToSpawn, objectTransform.position, objectTransform.rotation);

        if (gameObject.name == "BigBoulder")
            Player.player.AddDeltaItems("Dynamite", -1);
    }

    protected override bool ShouldHighlight()
    {
        return Player.player.GetAmountOfItem(itemToDestroyThis) >= 1;
    }
}