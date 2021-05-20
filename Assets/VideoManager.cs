using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer vPlayer;

    [SerializeField]
    private GameObject skipOption;
    private Image greenRing;

    private void Awake()
    {
        greenRing = skipOption.GetComponentInChildren<Image>();
        vPlayer.loopPointReached += (video) => { video.Stop(); SceneManager.LoadScene("SampleScene"); };
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            skipOption.SetActive(true);
            greenRing.fillAmount += 0.0015F;
        }
        else
        {
            greenRing.fillAmount -= 0.005F;
            if (greenRing.fillAmount <= 0)
                skipOption.SetActive(false);
        }

        if (greenRing.fillAmount >= 1)
        {
            vPlayer.Stop();
            SceneManager.LoadScene("SampleScene");
        }
    }
}
