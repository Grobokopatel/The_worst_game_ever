using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Player player;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Move();
    }

    private void Move()
    {
        transform.position = player.transform.position + transform.right * (Input.GetAxis("Horizontal") < 0 ? -1 : 1) * 0.5F + transform.up * (-0.5F);
    }
}
