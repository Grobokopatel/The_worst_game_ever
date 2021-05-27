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
                PopUpTextCreator.TextsToPopUp.Enqueue((phrases[new System.Random().Next(0, phrases.Length)], Color.white));
        }
        else
        {
            PopUpTextCreator.TextsToPopUp.Enqueue(($"��� ���� �� ��������, ������ ����� �����-�� ������", Color.white));
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
                        PopUpTextCreator.TextsToPopUp.Enqueue(($"���, � ���-�� �����", Color.white));
                        currentReload = reload;
                    }
                    else
                    {
                        Debug.Log("� ���");
                        PopUpTextCreator.TextsToPopUp.Enqueue(($"� ������ �� �������", Color.white));
                        currentReload = reload;
                    }
                }
                else
                {
                    PopUpTextCreator.TextsToPopUp.Enqueue(($"� �����, ��� �� ����������� ��� ������� {Mathf.CeilToInt(currentReload)}", Color.white));
                }
            }
            else
            {
                PopUpTextCreator.TextsToPopUp.Enqueue(($"��� ����� ������", Color.white));
            }
    }
}
