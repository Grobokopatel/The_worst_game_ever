using UnityEngine;

public class Log : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var player = otherCollider.GetComponent<Player>();

        if (player)
        {
            player.AddDeltaResources(InGameResources.Wood, 1);
            Destroy(gameObject);
            Debug.Log($"Количество брёвен в инвентаре: {player.GetAmountOfResource(InGameResources.Wood)}");
        }
    }
}