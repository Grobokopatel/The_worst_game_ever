using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch1 : Diggable
{
    public override void TryDig()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        GetComponent<Hatch>().IsDugOut = true;
        Destroy(GetComponent<Hatch1>());
    }
}
