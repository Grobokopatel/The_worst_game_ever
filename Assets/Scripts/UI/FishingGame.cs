using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class FishingGame : MonoBehaviour
{
    [SerializeField] private GameObject gridForArrows;
    [SerializeField] private GameObject arrowPrefab;
    private readonly List<(GameObject arrow, KeyCode key)> arrowsInfo = new List<(GameObject, KeyCode)>();
    private int currentArrowIndex;
    private (GameObject arrow, KeyCode key) currentArrowInfo;
    public static KeyCode[] arrowsKeyCodes = Enumerable.Range(273, 4).Select(number => (KeyCode)number).ToArray();
    private float timeToCaught;
    private float leftTime;

    private Color currentButtonColor = Color.red;
    [SerializeField] private Image leftPartProgressBar;
    [SerializeField] private Image rightPartProgressBar;
    [SerializeField] private Bobber playersBobber;
    private Bubbles bubblesThatBobberTouches;
    private int arrowNumber;
    private bool doesGameStarted = false;

    public void InstantiateGame()
    {
        bubblesThatBobberTouches =
            Technical.GetCollidersInPosition(playersBobber.transform.position)
         .Select(collider => collider.gameObject.GetComponent<Bubbles>())
         .First(component => component != null);

        var randomNumberGenerator = new System.Random();

        if (bubblesThatBobberTouches.MaxArrowAmount == 0)
            arrowNumber = randomNumberGenerator.Next(12, 20);
        else
            arrowNumber = randomNumberGenerator.Next(bubblesThatBobberTouches.MinArrowAmount, bubblesThatBobberTouches.MaxArrowAmount);

        for (var i = 0; i < arrowNumber; ++i)
        {
            var arrow = Instantiate(arrowPrefab, gridForArrows.transform);
            var turnAmount = randomNumberGenerator.Next(4);
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
        leftPartProgressBar.enabled = true;
        rightPartProgressBar.enabled = true;

        timeToCaught = bubblesThatBobberTouches.SecondsPerArrow * arrowNumber;
        leftTime = timeToCaught;

        enabled = true;
    }


    private void Start()
    {
        var timeBeforeGameStarts = new System.Random().Next(3, 5);

        playersBobber = Player.player.GetComponentInChildren<Bobber>();
        StartCoroutine(Technical.WaitThenInvokeMethod(timeBeforeGameStarts, () => doesGameStarted = true));
        StartCoroutine(WhatToDoBeforeGameInstantiation());
        enabled = false;
    }

    private IEnumerator WhatToDoBeforeGameInstantiation()
    {
        while (!doesGameStarted)
        {
            if (Input.GetButton("Horizontal") || Input.GetKeyDown(KeyCode.Escape))
            {
                Player.player.State = PlayerState.Idle;
                Destroy(gameObject);
            }
            yield return new WaitForEndOfFrame();
        }

        if (playersBobber.DoesTouchAnyBubbles())
        {
            InstantiateGame();
        }
        else
        {
            Player.player.State = PlayerState.Idle;
            PopUpTextCreator.QueueText($"Кажется, здесь не клюёт", Color.red);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Player.player.State = PlayerState.Idle;
            Destroy(gameObject);
            return;
        };

        leftTime -= Time.deltaTime;
        leftPartProgressBar.fillAmount = leftTime / timeToCaught;
        rightPartProgressBar.fillAmount = leftTime / timeToCaught;
        if (leftTime <= 0)
        {
            AudioManager.PlayAudio(AudioManager.FailSound);
            Destroy(gameObject);
            Player.player.State = PlayerState.Idle;
            return;
        }

        if (currentArrowIndex <= arrowsInfo.Count && arrowsKeyCodes.Any(keyCode => Input.GetKeyDown(keyCode)))
        {
            if (Input.GetKeyDown(currentArrowInfo.key))
            {
                Debug.Log("Нужная клавиша");

                currentArrowInfo.arrow.GetComponent<Image>().color = Color.white;
                if (currentArrowIndex != arrowsInfo.Count)
                {
                    AudioManager.PlayAudio(AudioManager.RightArrow);
                    currentArrowInfo = arrowsInfo[currentArrowIndex];
                    currentArrowInfo.arrow.GetComponent<Image>().color = currentButtonColor;
                    ++currentArrowIndex;
                }
                else
                {
                    var prize = bubblesThatBobberTouches.GetRandomItem();
                    Player.player.AddDeltaItems(prize, 1);
                    AudioManager.PlayAudio(AudioManager.CatchSound);
                    Player.player.State = PlayerState.Idle;
                    Destroy(gameObject);
                }
            }
            else
            {
                Player.player.State = PlayerState.Idle;
                Debug.Log("Не та клавиша");
                AudioManager.PlayAudio(AudioManager.FailSound);
                Destroy(gameObject);
            }
        }
    }
}
