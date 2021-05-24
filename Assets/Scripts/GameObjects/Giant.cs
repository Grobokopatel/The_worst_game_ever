using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        camera.orthographicSize = 3.5F;
        camera.GetComponent<CameraController>().YCoord = -3;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        camera.orthographicSize = 3;
        camera.GetComponent<CameraController>().YCoord = CameraController.constantYCoord;
    }
}
