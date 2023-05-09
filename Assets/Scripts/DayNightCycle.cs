using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    public float cycleDuration = 10.0f;
    public float maxIntensity = 1.0f; 
    public float minIntensity = 0.0f; 

    private float timeElapsed = 0.0f; 

    [SerializeField] private Light2D globalLight;

    void Start()
    {
        cycleLight();
    }


    public void cycleLight()
    {
        timeElapsed += Time.deltaTime;

        float intensity = Mathf.Lerp(maxIntensity, minIntensity, timeElapsed / cycleDuration);

        globalLight.intensity = intensity;

        if (timeElapsed >= cycleDuration)
        {
            timeElapsed = 0.0f;
        }
    }
}