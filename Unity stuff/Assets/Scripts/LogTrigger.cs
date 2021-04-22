using UnityEngine;

public class LogTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = FindObjectOfType<Player>();
        player.AddDeltaResources(InGameResources.Wood, 1);
        Destroy(GameObject.Find(gameObject.name));
        Debug.Log($"Кол-во деревьев в инвентаре: {player.GetAmountOfResource(InGameResources.Wood)}");
    }
}