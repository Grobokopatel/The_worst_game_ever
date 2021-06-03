using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private RectTransform textTransform;

    void Start()
    {
        StartCoroutine(Technical.WaitThenInvokeMethod(40, () => Destroy(gameObject)));
    }


    void Update()
    {
        textTransform.Translate(Time.deltaTime * transform.up * 146);
    }
}
