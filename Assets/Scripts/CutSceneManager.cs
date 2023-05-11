using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CutSceneManager : MonoBehaviour
{
    public Sprite[] frames;
    public float frameDuration = 0.1f;
    private int currentFrame = 0;
    public Image display;

    public void StartCutscene(Sprite[] newFrames)
    {
        frames = newFrames;
        currentFrame = 0;
        gameObject.SetActive(true);
        StartCoroutine(ShowFrames());
    }

    private IEnumerator ShowFrames()
    {
        while (currentFrame < frames.Length)
        {
            display.sprite = frames[currentFrame];
            yield return new WaitForSeconds(frameDuration);
            currentFrame++;
        }
        gameObject.SetActive(false);
    }
}
