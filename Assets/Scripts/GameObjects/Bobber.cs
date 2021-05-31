using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    public bool DoesTouchAnyBubbles()
    {
        return Technical.GetCollidersInPosition(transform.position)
            .Any(collider => collider.GetComponent<Bubbles>());
    }

    public Item TryToCatchItem()
    {
        return Technical.GetCollidersInPosition(transform.position)
            .Select(collider => collider.GetComponent<Bubbles>())
            .Where(circlesOnWater => circlesOnWater != null)
            .FirstOrDefault()
            .GetRandomItem();
    }
}
