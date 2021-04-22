using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target)
            target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + transform.forward * (-10) + transform.up * 1.4F, speed * Time.deltaTime);
    }
}
