using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsLinks : MonoBehaviour
{
    public static Player Player
    { 
        get;
        set;
    }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }
}
