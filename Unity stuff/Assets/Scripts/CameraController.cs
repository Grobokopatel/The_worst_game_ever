using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer intro;

    private bool isIntroPlaying = true;

    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    private Vector3 offsetVector;

    private void Start()
    {
        /*intro.isLooping = false;
        intro.Play();*/
    }

    private void OnGUI()
    {
        /*if (isIntroPlaying)
        {
            if (!intro.isPlaying)
            {
                SceneManager.LoadScene(1);
                isIntroPlaying = false;
            }
        }*/
    }


    private void Awake()
    {
        offsetVector = transform.forward * (-13F) + transform.up * 1.3F;
        if (!target)
            target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offsetVector, speed * Time.deltaTime);
    }
}
