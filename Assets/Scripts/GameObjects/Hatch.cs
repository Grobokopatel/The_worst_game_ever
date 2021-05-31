using UnityEngine;
using System;

public class Hatch : Interactable
{
    public bool isDugOut;
    private float currentReload = 0;
    private readonly float reload = 4;
    [SerializeField] private GameObject autro;
    private string[] phrases = {
        "Я думал будет смешнее",
        "Я ненавижу аниме",
        "Эх, вот бы в финал пройти",
        "Чувак, ты думал что-то здесь будет?",
        "Интересно, они вправду эту игру за 5 часов сделали?"
    };

    public override void Interact()
    {
        if (Player.player.GetAmountOfItem("Key") >= 1)
        {
            if (autro != null)
            {
                Debug.Log("Конец игры");
                Player.player.GetComponent<AudioSource>().Stop();
                autro.SetActive(true);
            }
            else
            {
                PopUpTextCreator.QueueText(phrases[new System.Random().Next(0, phrases.Length)]);
            }
        }
        else
        {
            PopUpTextCreator.QueueText($"Мой ключ не подходит, видимо нужен какой-то другой");
        }
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
                    AudioManager.PlayAudio(AudioManager.DigSound);
                    if (Mathf.Abs(transform.position.x - Player.player.transform.position.x) <= 1 && !isDugOut)
                    {
                        isDugOut = true;
                        GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 1);
                        PopUpTextCreator.QueueText($"Так, я что-то нашёл");
                        currentReload = reload;
                    }
                    else
                    {
                        PopUpTextCreator.QueueText($"Я ничего не откопал");
                        currentReload = reload;
                    }
                }
                else
                {
                    PopUpTextCreator.QueueText($"Я устал, мне бы передохнуть ещё секунды {Mathf.CeilToInt(currentReload)}");
                }
            }
            else
            {
                PopUpTextCreator.QueueText($"Мне нечем копать");
            }
    }
}
