using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    public void startGame()
    {
        print("aaaaaaa");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
