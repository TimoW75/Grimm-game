using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour
{
    public Sprite[] frames;
    public float frameDuration = 0.1f;
    private int currentFrame = 0;
    public Image display;
    public AudioSource soundEffectCutScene;
    [SerializeField] bool lastImageTakesLonger = false;

    public void StartCutscene(Sprite[] newFrames)
    {
        frames = newFrames;
        currentFrame = 0;
        gameObject.SetActive(true);
        StartCoroutine(ShowFrames());
    }

    private IEnumerator ShowFrames()
    {
        if (soundEffectCutScene)
        {
            StartCoroutine(Fade(true, soundEffectCutScene, 3f, 0.4f));
            GameObject BackgroundMusic = GameObject.FindGameObjectWithTag("Audio");
            if(BackgroundMusic != null)
            {
                BackgroundMusic.GetComponent<AudioSource>().Pause();
            }

        }
        while (currentFrame < frames.Length)
        {
            display.sprite = frames[currentFrame];
            if (currentFrame == frames.Length - 1 && lastImageTakesLonger)
            {
                yield return new WaitForSeconds(frameDuration * 15);
            }
            else
            {
                yield return new WaitForSeconds(frameDuration);
            }
            currentFrame++;
        }
        if (soundEffectCutScene)
        {
            soundEffectCutScene.Stop();
            GameObject BackgroundMusic = GameObject.FindGameObjectWithTag("Audio");
            if (BackgroundMusic != null)
            {
                BackgroundMusic.GetComponent<AudioSource>().Play();
            }

        }
        gameObject.SetActive(false);
    }
    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        source.Play();
        if (!fadeIn)
        {
            double lengtOfSource = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)lengtOfSource - duration);
        }

        float time = 0f;
        float startVol = source.volume;
        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }

        yield break;
    }
}
