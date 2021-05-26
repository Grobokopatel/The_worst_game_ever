using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AutroManager : MonoBehaviour
{
    public VideoPlayer vPlayer;
    [SerializeField] private GameObject skipOption;
    private Image greenRing;

    private void Awake()
    {
        greenRing = skipOption.GetComponentInChildren<Image>();
        vPlayer.loopPointReached += (video) =>
        {
            video.Stop();
            Destroy(transform.parent.gameObject);
            Player.player.enabled = true;
        };
        Player.player.enabled = false;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            skipOption.SetActive(true);
            greenRing.fillAmount += 0.0070F;
        }
        else
        {
            greenRing.fillAmount -= 0.03F;
            if (greenRing.fillAmount <= 0)
                skipOption.SetActive(false);
        }

        if (greenRing.fillAmount >= 1)
        {
            vPlayer.Stop();
            Destroy(transform.parent.gameObject);
            Player.player.enabled = true;
        }
    }
}