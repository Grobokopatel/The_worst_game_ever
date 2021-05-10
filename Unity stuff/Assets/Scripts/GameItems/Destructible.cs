using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

public class Destructible : InteractableObject
{
    [SerializeField]
    private GameObject toSpawn;

    public override void Interact(Player player)
    {
        var lastPosition = gameObject.transform;
        Destroy(gameObject);
        Instantiate(toSpawn, transform.position, lastPosition.rotation);
    }
}