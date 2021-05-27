using UnityEngine;

public class Hatch : Interactable
{
    public bool isDugOut = false;
    private float currentReload = 0;
    private float reload = 4;
    [SerializeField] private GameObject autro;

    public override void Interact()
    {
        if (Player.player.GetAmountOfItem("Key") >= 1)
        {
            Debug.Log("Конец игры");
            autro.SetActive(true);
        }
        else
        {
            PopUpTextCreator.TextsToPopUp.Enqueue(($"Мой ключ не подходит, видимо нужен какой-то другой", Color.white));
        }
    }

    private void Awake()
    {
        base.Awake();
    }

    protected override bool ShouldHighlight()
    {
        return isDugOut;
    }

    void Update()
    {
        if (currentReload > 0)
            currentReload -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.G))
            if (Player.player.GetAmountOfItem("Shovel") >= 1)
            {
                if (currentReload <= 0)
                {
                    if (Mathf.Abs(transform.position.x - Player.player.transform.position.x) <= 1 && !isDugOut)
                    {
                        isDugOut = true;
                        GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                        PopUpTextCreator.TextsToPopUp.Enqueue(($"Так, я что-то нашёл", Color.white));
                        currentReload = reload;
                    }
                    else
                    {
                        PopUpTextCreator.TextsToPopUp.Enqueue(($"Я ничего не выкопал", Color.white));
                        currentReload = reload;
                    }
                }
                else
                {
                    PopUpTextCreator.TextsToPopUp.Enqueue(($"Я устал, мне бы передохнуть ещё секунды {Mathf.CeilToInt(currentReload)}", Color.white));
                }
            }
            else
            {
                PopUpTextCreator.TextsToPopUp.Enqueue(($"Мне нечем копать", Color.white));
            }
    }
}
