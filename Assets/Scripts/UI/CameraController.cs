using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CameraController : MonoBehaviour
{
    public static CameraController cameraController;
    [SerializeField]
    private VideoPlayer intro;

    private bool isIntroPlaying = true;

    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float yOffset;
    private float zOffset = -10F;
    private float xOffset;

    public float XOffset
    {
        get => xOffset;
        set
        {
            xOffset = value;
        }
    }

    private void Awake()
    {
        if (!target)
            target = FindObjectOfType<Player>().transform;
        cameraController = this;
    }

    private void Update()
    {
        var offsetVector = new Vector3(xOffset, yOffset, zOffset);
        var offsettedTarget = target.position + offsetVector;
        offsettedTarget.y = -4.982996F;

        transform.position = Vector3.Lerp(transform.position, offsettedTarget, speed * Time.deltaTime);
    }
}
