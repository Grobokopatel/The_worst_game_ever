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

    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    public const float constantYCoord = -4.982996F;
    [SerializeField]
    private float yCoord = constantYCoord;
    public float YCoord
    {
        get => yCoord;
        set
        {
            yCoord = value;
        }
    }
    private const float zOffset = -10F;
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
        if (Player.player.currentReload > 0)
            Player.player.currentReload -= Time.deltaTime;
        var offsetVector = new Vector3(xOffset, 0, zOffset);
        var offsettedTarget = target.position + offsetVector;
        offsettedTarget.y = yCoord;

        transform.position = Vector3.Lerp(transform.position, offsettedTarget, speed * Time.deltaTime);
    }
}
