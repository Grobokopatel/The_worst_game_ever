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
    private readonly List<(GameObject arrow, KeyCode key)> arrowsInfo = new List<(GameObject, KeyCode)>();
    private int currentArrowIndex;
    private (GameObject arrow, KeyCode key) currentArrowInfo;
    public static KeyCode[] arrowsKeyCodes;

    static FishingGame()
    {
        arrowsKeyCodes = new[] { KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
    }
    
    public void InstantiateGame()
    {
        var rng = new System.Random();
        var arrowNumber = rng.Next(4, 15);
        for (var i = 0; i < arrowNumber; ++i)
        {
            var arrow = Instantiate(arrowPrefab, grid.transform);
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
        currentArrowInfo = arrowsInfo.FirstOrDefault();
        currentArrowIndex = 1;
        currentArrowInfo.arrow.GetComponent<Image>().color = Color.red;
    }

    private void Start()
    {
        enabled = false;
        StartCoroutine(Technical.Timer(new System.Random().Next(3, 6), () => { InstantiateGame(); enabled = true; }));
    }

    void Update()
    {
        if (currentArrowIndex <= arrowsInfo.Count && arrowsKeyCodes.Any(keyCode => Input.GetKeyDown(keyCode)))
        {
            if (Input.GetKeyDown(currentArrowInfo.key))
            {
                Debug.Log("������ �������");
                currentArrowInfo.arrow.GetComponent<Image>().color = Color.white;
                if (currentArrowIndex != arrowsInfo.Count)
                {
                    currentArrowInfo = arrowsInfo[currentArrowIndex];
                    currentArrowInfo.arrow.GetComponent<Image>().color = Color.red;
                    ++currentArrowIndex;
                }
                else
                {
                    Player.player.State = PlayerState.Idle;
                    Destroy(gameObject);
                }
            }
            else
            {
                Player.player.State = PlayerState.Idle;
                Debug.Log("�� �� �������");
                Destroy(gameObject);
            }
        }
    }
}
