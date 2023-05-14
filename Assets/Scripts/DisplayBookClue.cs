
using TMPro;
using UnityEngine;

public class DisplayBookClue : MonoBehaviour
{

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string objectClue;

    private void Start()
    {
        dialoguePanel.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = objectClue;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = string.Empty;
    }
}

