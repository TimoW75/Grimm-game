using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textComponent;
    public string[] lines;
    [SerializeField] private float textSpeed;

    private int index;
    private bool playerIsClose;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialgue();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartDialgue();

            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
        }

    }

    public void zeroText()
    {
        textComponent.text = string.Empty;
        index = 0;
        dialoguePanel.SetActive(false);
    }

    void StartDialgue()
    {
        index = 0;
        textComponent.text = lines[index];
    }

  
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            textComponent.text = lines[index];
        }
        else
        {
            zeroText() ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            dialoguePanel.SetActive(false);
        }
    }
}
