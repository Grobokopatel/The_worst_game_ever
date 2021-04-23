using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    private Vector3 offsetVector;

    private void Awake()
    {
        offsetVector = transform.forward * (-13F)  + transform.up*1.3F;
        if (!target)
            target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offsetVector, speed * Time.deltaTime);
    }
}
