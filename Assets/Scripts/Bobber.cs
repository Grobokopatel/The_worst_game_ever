using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public bool HasAnyCirclesOnIt()
    {
        return Player.GetCollidersInPosition(transform.position).Any(collider => collider.GetComponent<CirclesOnWater>());
    }

    public Item TryToCatchItem()
    {
        return Player.GetCollidersInPosition(transform.position)
            .Select(collider => collider.GetComponent<CirclesOnWater>())
            .Where(circlesOnWater => circlesOnWater != null)
            .FirstOrDefault()
            ?.GetRandomItem();
    }
}
