using UnityEngine;
using System;

public class Hatch : Interactable
{
    public bool IsDugOut
    {
        get;
        set;
    }
    
    private bool IsOpened
    {
        get;
        set;
    }

    private Animator animator;

    [SerializeField] private GameObject autro;
    private string[] phrases = {
        "� ����� ����� �������",
        "� �������� �����",
        "��, ��� �� � ����� ������",
        "�����, �� ����� ���-�� ����� �����?",
        "���������, ��� ������� ��� ���� �� 5 ����� �������?"
    };

    protected override void Initialize()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (!IsOpened)
        {
            if (Player.player.GetAmountOfItem("Key") >= 1)
            {
                IsOpened = true;
                Player.player.AddDeltaItems("Key", -1);
                PopUpTextCreator.QueueText("���� ���� �������");
                animator.SetBool("IsOpened", true);
                AudioManager.PlayAudio(AudioManager.HatchOpen);
            }
            else
            {
                PopUpTextCreator.QueueText($"��� ���� �� ��������, ������ ����� �����-�� ������");
            }
        }
        else
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
    }

    protected override bool ShouldHighlight()
    {
        return IsDugOut;
    }
}
