using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    private double timeToDisappear = 8;
    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        timeToDisappear -= Time.deltaTime;
        if (timeToDisappear <= 0)
            Destroy(gameObject);

        text.transform.Translate(transform.up * 0.005F);
    }
}