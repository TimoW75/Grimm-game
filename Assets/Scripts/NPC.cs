using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System.Collections;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicePannel;

    [SerializeField] private TextMeshProUGUI textComponent;
    public string[] lines;
    [SerializeField] private float textSpeed;

    private int index;
    private bool playerIsClose;

    void Start()
    {
        textComponent.text = string.Empty;
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.F) && playerIsClose)
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
        if (Input.GetKey(KeyCode.Return) && playerIsClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
        }
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
            print(index);
            index++;
            textComponent.text = "";
            StartCoroutine(Typing());
        }
    }
    IEnumerator Typing()
    {
        foreach (char letter in lines[index])
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void zeroText()
    {
        textComponent.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
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
        }
    }

}
