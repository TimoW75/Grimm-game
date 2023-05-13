using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public Image image; // Reference to the black image

    public float fadeDuration = 1f; 
    public float waitDuration = 1f; 

    private Coroutine fadeCoroutine;

    void Start()
    {
        Color imageColor = image.material.color;
        imageColor.a = 0f;
        image.material.color = imageColor;
    }
    public void startFade()
    {
        fadeCoroutine = StartCoroutine(StartFade());
    }

    IEnumerator StartFade()
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            image.material.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitDuration);

        // Fade out
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            image.material.color = new Color(0f, 0f, 0f, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // Call this method to stop the fading process
    public void StopFade()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
    }
}