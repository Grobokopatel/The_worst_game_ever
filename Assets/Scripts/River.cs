using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River : MonoBehaviour
{
    private Transform[] riverParts;
    [SerializeField]
    private Transform cameraTransform;

    private void Awake()
    {
        riverParts = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        foreach (var riverPart in riverParts)
        {
            var deltaX = cameraTransform.position.x - riverPart.position.x;
            if (Mathf.Abs(deltaX) >= 11.7)
            {
                riverPart.Translate((deltaX < 0 ? -1 : 1) * 20F * transform.right);
            }
        }
    }
}
