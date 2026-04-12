using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] AudioClips;

    private int clipTracker = 0;

    private void Start()
    {
        clipTracker = 0;
    }

    public void PlayClip()
    {
        if (!AudioClips[clipTracker])
        {
            return;
        }

        AudioSource.PlayClipAtPoint(AudioClips[clipTracker], Camera.main.transform.position);
        UptickClipTracker();
    }

    private void UptickClipTracker()
    {
        if (clipTracker + 1 >= AudioClips.Length)
        {
            clipTracker = 0;
        } else
        {
            clipTracker++;
        }
    }
}
