using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var player = otherCollider.GetComponent<Player>();

        if (player)
        {
            player.AddDeltaResources(InGameResources.Log, 1);
            Destroy(gameObject);
            Debug.Log($"Количество брёвен в инвентаре: {player.GetAmountOfResource(InGameResources.Log)}");
        }
    }
}
