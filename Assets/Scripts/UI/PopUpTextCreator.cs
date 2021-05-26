using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextCreator : MonoBehaviour
{
    public static GameObject PopUpText;
    private float timeBetweenPopUpTexts = 0.4F;
    private float currentTime;
    public static Queue<(string, Color)> TextsToPopUp
    {
        get;
        set;
    }

    private void Awake()
    {
        TextsToPopUp = new Queue<(string, Color)>();
        currentTime = timeBetweenPopUpTexts;
        PopUpText = Resources.Load<GameObject>("Prefabs/PopUpText");
    }

    private static void CreateText(string text, Color color)
    {
        var floatingText = Instantiate(PopUpText, Player.player.transform.position, Player.player.transform.rotation);
        floatingText.GetComponentInChildren<Text>().text = text;
        floatingText.GetComponentInChildren<Text>().color = color;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0 && TextsToPopUp.Count != 0)
        {
            var nextText = TextsToPopUp.Dequeue();
            CreateText(nextText.Item1, nextText.Item2);
            currentTime = timeBetweenPopUpTexts;
        }
    }
}
