using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public bool questActiveGeneral;
    [SerializeField] public string currentQuest;
    [SerializeField] public GameObject QuestActiveTextBox;
    [SerializeField] public TextMeshProUGUI questText;
    [SerializeField] public int dayNumber;
    void Start()
    {
        questActiveGeneral = false;
        currentQuest = string.Empty;
        QuestActiveTextBox.SetActive(false);
    }

    public void setTextActive()
    {
        QuestActiveTextBox.SetActive(true);
        questText.text = currentQuest;
    }
    public void setTextHiden()
    {
        QuestActiveTextBox.SetActive(false);
        questText.text = string.Empty;

    }

    public void newDay()
    {
        dayNumber++;
    }

}
