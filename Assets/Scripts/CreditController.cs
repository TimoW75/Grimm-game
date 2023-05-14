using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditController : MonoBehaviour
{
    public CutSceneManager[] cutsceneManagers;

    private void Start()
    {
        PlayCutscene(0);
    }

    public void PlayCutscene(int index)
    {
        if (index >= 0 && index < cutsceneManagers.Length)
        {
            cutsceneManagers[index].StartCutscene(cutsceneManagers[index].frames);
        }
    }
}
