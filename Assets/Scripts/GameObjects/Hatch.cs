using UnityEngine;
using System;

public class Hatch : Interactable
{
    public bool isDugOut;
    private float currentReload = 0;
    private readonly float reload = 4;
    [SerializeField] private GameObject autro;
    private string[] phrases = {
        "� ����� ����� �������",
        "� �������� �����",
        "��, ��� �� � ����� ������",
        "�����, �� ����� ���-�� ����� �����?",
        "���������, ��� ������� ��� ���� �� 5 ����� �������?"
    };

    public override void Interact()
    {
        if (Player.player.GetAmountOfItem("Key") >= 1)
        {
            if (autro != null)
            {
                Debug.Log("����� ����");
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
            PopUpTextCreator.QueueText($"��� ���� �� ��������, ������ ����� �����-�� ������");
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
                        PopUpTextCreator.QueueText($"���, � ���-�� �����");
                        currentReload = reload;
                    }
                    else
                    {
                        PopUpTextCreator.QueueText($"� ������ �� �������");
                        currentReload = reload;
                    }
                }
                else
                {
                    PopUpTextCreator.QueueText($"� �����, ��� �� ����������� ��� ������� {Mathf.CeilToInt(currentReload)}");
                }
            }
            else
            {
                PopUpTextCreator.QueueText($"��� ����� ������");
            }
    }
}
