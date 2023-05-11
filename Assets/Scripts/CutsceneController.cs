using UnityEngine;

public class CutsceneController : MonoBehaviour
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