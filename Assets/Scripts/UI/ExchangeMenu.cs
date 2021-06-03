using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeMenu : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E))
        {
            gameObject.SetActive(false);
            Player.player.enabled = true;
            cameraController.XOffset = 0;
        }
    }
}
