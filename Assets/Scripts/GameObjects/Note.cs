using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : Interactable
{
    [SerializeField]
    private GameObject noteText;

    protected override void Initialize()
    {
        enabled = false;
    }

    public override void Interact(Player player)
    {
        noteText.SetActive(true);
        enabled = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            enabled = false;
            noteText.SetActive(false);
        }
    }
}
