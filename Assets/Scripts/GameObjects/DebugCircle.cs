using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCircle : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Move();
    }

    private void Move()
    {
        transform.position = player.transform.position + 0.5F * (Input.GetAxis("Horizontal") < 0 ? -1 : 1) * transform.right + (-0.5F) * transform.up;
    }
}
