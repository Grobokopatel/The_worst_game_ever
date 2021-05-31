using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpTextCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    public static GameObject PopUpTextPrefab;
    private float timeBetweenPopUpTexts = 0.27F;
    private float currentTime = 0;
    public static Queue<(string text, Color color)> TextsToPopUp
    {
        get;
        set;
    }

    private void Awake()
    {
        TextsToPopUp = new Queue<(string, Color)>();
        PopUpTextPrefab = Resources.Load<GameObject>("Prefabs/UI/PopUpText");
    }

    public static void QueueText(string text, Color color)
    {
        TextsToPopUp.Enqueue((text, color));
    }

    public static void QueueText(string text)
    {
        TextsToPopUp.Enqueue((text, Color.white));
    }

    private static void CreateText(string text, Color color)
    {
        var floatingText = Instantiate(PopUpTextPrefab, Player.player.transform.position, Player.player.transform.rotation);
        floatingText.GetComponentInChildren<Text>().text = text;
        floatingText.GetComponentInChildren<Text>().color = color;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SetActive(!inventory.activeInHierarchy);
        }

        if (currentTime > 0)
            currentTime -= Time.deltaTime;

        if (currentTime <= 0 && TextsToPopUp.Count != 0)
        {
            currentTime = timeBetweenPopUpTexts;
            var nextText = TextsToPopUp.Dequeue();
            CreateText(nextText.text, nextText.color);
        }
    }
}
