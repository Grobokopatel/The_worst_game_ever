using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfDirt : Diggable
{
    public override void TryDig()
    {
        Instantiate(itemToDrop, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
