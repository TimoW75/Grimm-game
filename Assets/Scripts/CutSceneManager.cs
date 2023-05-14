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
    public AudioSource BackgroundMusic;

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
            BackgroundMusic.Pause();
        }
        while (currentFrame < frames.Length)
        {
            display.sprite = frames[currentFrame];
            yield return new WaitForSeconds(frameDuration);
            currentFrame++;
        }
        if (soundEffectCutScene)
        {
            soundEffectCutScene.Stop();
            BackgroundMusic.Play();
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
