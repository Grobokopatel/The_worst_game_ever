using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Diggable : MonoBehaviour
{
    [SerializeField] protected GameObject itemToDrop;

    public abstract void TryDig();
}
