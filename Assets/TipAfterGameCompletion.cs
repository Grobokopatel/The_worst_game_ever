using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipAfterGameCompletion : MonoBehaviour
{
    private const float timeBeforeDestroying = 10;
    private float currentTimeBeforeDestroying;

    private void Awake()
    {
        currentTimeBeforeDestroying = timeBeforeDestroying;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
