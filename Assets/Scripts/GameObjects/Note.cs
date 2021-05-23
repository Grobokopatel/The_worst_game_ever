using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Interactable
{
    [SerializeField]
    private GameObject noteText;

    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    public override void Interact()
    {
        StartCoroutine(Technical.WaitThenInvokeMethod(0, () =>
        {
            Player.player.State = PlayerState.Idle;
            Player.player.enabled = false;
        }));

        noteText.SetActive(true);
        enabled = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            enabled = false;
            noteText.SetActive(false);
            Player.player.enabled = true;
        }
    }
}
