using System.Collections.Generic;
using UnityEngine;

public class Destructible : InteractableObject
{
    [SerializeField]
    private GameObject toSpawn;
    private static readonly Dictionary<string, Item> itemsToDestroyObject = new Dictionary<string, Item>();

    public override void Awake()
    {
        Sprite = GetComponentInChildren<SpriteRenderer>();
        itemsToDestroyObject["Boulder"] = Technical.GetItem("Pickaxe");
        itemsToDestroyObject["Tree"] = Technical.GetItem("Axe");
    }

    public override void Interact(Player player)
    {
        var lastPosition = gameObject.transform;
        Destroy(gameObject);
        Instantiate(toSpawn, transform.position, lastPosition.rotation);
    }

    public override bool ShouldHighlight(Player player)
    {
        return player.GetAmountOfItem(itemsToDestroyObject[gameObject.name.GetItemNameWithoutAdditInfo()]) >= 1;
    }
}