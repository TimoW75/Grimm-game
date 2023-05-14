using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public AudioClip audioClip;
    public float volume = 1.0f;
    public int playCount = 1;

    private AudioSource audioSource;

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
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            audioSource.Play();

            if (playCount > 1)
            {
                StartCoroutine(PlayAudioMultipleTimes());
            }
        }
    }

    private System.Collections.IEnumerator PlayAudioMultipleTimes()
    {
        int count = 1;
        while (count < playCount)
        {
            yield return new WaitForSeconds(audioSource.clip.length);
            audioSource.Play();
            count++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger");
            audioSource.Stop();
            StopAllCoroutines();
        }
    }
}
