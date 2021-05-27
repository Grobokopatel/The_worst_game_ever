using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip CollectSound, DestructSound, MoveSound, CatchSound, FailSound, CraftSound, TradeSound;
    private static AudioSource audioSource;

    private void Start()
    {
        CollectSound = Resources.Load<AudioClip>("Audio/collect");
        DestructSound = Resources.Load<AudioClip>("Audio/destruct");
        MoveSound = Resources.Load<AudioClip>("Audio/move");
        CatchSound = Resources.Load<AudioClip>("Audio/catch");
        FailSound = Resources.Load<AudioClip>("Audio/fail");
        CraftSound = Resources.Load<AudioClip>("Audio/craft");
        TradeSound = Resources.Load<AudioClip>("Audio/trade");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}