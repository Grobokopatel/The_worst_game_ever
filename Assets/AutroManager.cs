using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AutroManager : MonoBehaviour
{
    public VideoPlayer vPlayer; 
    [SerializeField] private GameObject skipOption;
    private Image greenRing;
    [SerializeField] private GameObject tipAfterGameCompletion;
    [SerializeField] private GameObject credits;

    private void Awake()
    {
        enabled = false;
        StartCoroutine(Technical.WaitThenInvokeMethod(0.3F, () => enabled = true));
        StartCoroutine(Technical.WaitThenInvokeMethod(42F, () => credits.SetActive(true)));
        greenRing = skipOption.GetComponentInChildren<Image>(true);
        vPlayer.loopPointReached += (video) =>
        {
            tipAfterGameCompletion.SetActive(true);
            video.Stop();
            Player.player.GetComponent<AudioSource>().Play();
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
            greenRing.fillAmount += Time.deltaTime * 0.84F;
        }
        else
        {
            greenRing.fillAmount -= Time.deltaTime * 1.8F;
            if (greenRing.fillAmount <= 0)
                skipOption.SetActive(false);
        }

        if (greenRing.fillAmount >= 1)
        {
            tipAfterGameCompletion.SetActive(true);
            vPlayer.Stop();
            Player.player.GetComponent<AudioSource>().Play();
            Destroy(transform.parent.gameObject);
            Player.player.enabled = true;
            Destroy(credits);
        }
    }
}