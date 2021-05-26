using UnityEngine;

public class Hatch : Interactable
{
    public bool isDugOut = false;
    [SerializeField] private GameObject autro;

    public override void Interact()
    {
        if(Player.player.GetAmountOfItem("Key")>=1)
        {
            Debug.Log("Конец игры");
            autro.SetActive(true);
        }
    }

    protected override bool ShouldHighlight()
    {
        return isDugOut;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)
            && Player.player.GetAmountOfItem("Shovel") >= 1
            && Mathf.Abs(transform.position.x - Player.player.transform.position.x) <= 1)
        {
            isDugOut = true;
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }
    }
}
