using System.Collections.Generic;
using UnityEngine;

public class Destructible : Interactable
{
    [SerializeField]
    private GameObject toSpawn;
    private static readonly Dictionary<string, Item> itemsToDestroyObject = new Dictionary<string, Item>();

    protected override void Awake()
    {
        base.Awake();
        itemsToDestroyObject["Boulder"] = Technical.GetItem("Pickaxe");
        itemsToDestroyObject["Tree"] = Technical.GetItem("Axe");
        itemsToDestroyObject["BigBoulder"] = Technical.GetItem("Dynamite");
    }
    public override void Interact(Player player)
    {
        var objectTransform = gameObject.transform;
        Destroy(gameObject);
        if (toSpawn != null)
            Instantiate(toSpawn, objectTransform.position, objectTransform.rotation);

        if (gameObject.name == "BigBoulder")
            Player.player.AddDeltaItems("Dynamite", -1);
    }

    protected override bool ShouldHighlight(Player player)
    {
        return player.GetAmountOfItem(itemsToDestroyObject[gameObject.name.GetItemNameWithoutAdditInfo()]) >= 1;
    }
}