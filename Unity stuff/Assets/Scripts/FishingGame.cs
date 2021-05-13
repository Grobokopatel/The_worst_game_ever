using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class FishingGame : MonoBehaviour
{
    [SerializeField]
    private GameObject grid;

    [SerializeField]
    private GameObject arrowPrefab;
    private List<(GameObject arrow, KeyCode key)> arrowsInfo = new List<(GameObject, KeyCode)>();

    public void Awake()
    {
        //Player.player.enabled = false;
        int arrowNumber = 19;
        var rng = new System.Random();
        for (var i = 0; i < arrowNumber; ++i)
        {//273-276 ^ !^ > <
            var arrow = Instantiate(arrowPrefab, grid.transform); //< !^ > ^
            var turnAmount = rng.Next(4);
            arrow.GetComponent<Image>().transform.Rotate(0, 0, 90 * turnAmount, Space.Self);
            KeyCode key;
            switch (turnAmount)
            {
                case 0:
                    key = KeyCode.LeftArrow;
                    break;
                case 1:
                    key = KeyCode.DownArrow;
                    break;
                case 2:
                    key = KeyCode.RightArrow;
                    break;
                default:
                    key = KeyCode.UpArrow;
                    break;
            }

            arrowsInfo.Add((arrow, key));
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (var arrowInfo in arrowsInfo)
        {
            var arrowImage = arrowInfo.arrow.GetComponent<Image>();
            arrowImage.color = Color.red;
            while (true)
            {
                if (Input.GetKeyDown(arrowInfo.key))
                {
                    Debug.Log("Нужная клавиша");
                    arrowImage.color = Color.white;
                    break;
                }
                /*else if(Input.GetKeyDown(arrowInfo.key))
                {
                    Debug.Log("Не та клавиша");
                    arrowImage.color = Color.white;
                    break;
                }*/
            }
        }
    }
}
