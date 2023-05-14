using UnityEngine;

public class AUdioCow : MonoBehaviour
{
 public AudioClip audioClip;
    public float volume = 1.0f;
    public float minDelayBetweenPlays = 6.0f;

    private AudioSource audioSource;
    private bool canPlay = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = audioClip;
        audioSource.volume = volume;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canPlay)
        {
            Debug.Log("Player entered trigger");
            PlayAudioClip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
        }
    }

    private void PlayAudioClip()
    {
        audioSource.Play();
        canPlay = false;
        Invoke(nameof(ResetCanPlay), minDelayBetweenPlays);
    }

    private void ResetCanPlay()
    {
        canPlay = true;
    }
}