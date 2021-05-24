using UnityEngine;
using UnityEngine.UI;

public class TextAbovePlayer : MonoBehaviour
{
    public Text Text;
    public double TimeToDisappear = 8;

    public void Update()
    {
        TimeToDisappear -= Time.deltaTime;
        if (TimeToDisappear <= 0)
            Destroy(gameObject);

        transform.Translate(transform.up * 0.01F);
        //var color = Text.color;
        //color.a -= 0.01f;
        //Text.color = color;
    }
}