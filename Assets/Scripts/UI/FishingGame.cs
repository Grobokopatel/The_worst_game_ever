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
    public static KeyCode[] arrowsKeyCodes= Enumerable.Range(273, 4).Select(number => (KeyCode)number).ToArray();
    private float timeToCaught;
    private float leftTime;
    private Timer timer = new Timer(new System.Random().Next(0));
    private Color currentButtonColor = Color.red;
    [SerializeField]
    private Image leftPartProgressBar;
    [SerializeField]
    private Image rightPartProgressBar;
    [SerializeField]
    private Bobber playersBobber;

    public void InstantiateGame()
    {
        var rng = new System.Random();
        var arrowNumber = rng.Next(12, 20);
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
        currentArrowInfo.arrow.GetComponent<Image>().color = currentButtonColor;
        timeToCaught = 0.35F * arrowNumber;
        leftTime = timeToCaught;
        leftPartProgressBar.enabled = true;
        rightPartProgressBar.enabled = true;
    }

    private void Start()
    {
        playersBobber = Player.player.GetComponentInChildren<Bobber>();
    }

    void Update()
    {
        if (timer.LeftTime > 0)
        {
            if (Input.GetButton("Horizontal"))
            {
                Player.player.State = PlayerState.Run;
                Destroy(gameObject);
            }

            return;
        }
        if (timer.WasOverOnThisFrame())
        {
            if (!playersBobber.HasAnyCirclesOnIt())
            {
                Player.player.State = PlayerState.Idle;
                Debug.Log("Не клюёт");
                Destroy(gameObject);
                return;
            }
            InstantiateGame();
        }

        leftTime -= Time.deltaTime;
        leftPartProgressBar.fillAmount = leftTime / timeToCaught;
        rightPartProgressBar.fillAmount = leftTime / timeToCaught;
        if (leftTime <= 0)
        {
            Destroy(gameObject);
            Player.player.State = PlayerState.Idle;
        }

        if (currentArrowIndex <= arrowsInfo.Count && arrowsKeyCodes.Any(keyCode => Input.GetKeyDown(keyCode)))
        {
            if (Input.GetKeyDown(currentArrowInfo.key))
            {
                Debug.Log("Нужная клавиша");
                currentArrowInfo.arrow.GetComponent<Image>().color = Color.white;
                if (currentArrowIndex != arrowsInfo.Count)
                {
                    currentArrowInfo = arrowsInfo[currentArrowIndex];
                    currentArrowInfo.arrow.GetComponent<Image>().color = currentButtonColor;
                    ++currentArrowIndex;
                }
                else
                {
                    var prize = playersBobber.TryToCatchItem();
                    Player.player.AddDeltaItems(prize, 1);
                    Player.player.State = PlayerState.Idle;
                    Destroy(gameObject);
                }
            }
            else
            {
                Player.player.State = PlayerState.Idle;
                Debug.Log("Не та клавиша");
                Destroy(gameObject);
            }
        }
    }
}
