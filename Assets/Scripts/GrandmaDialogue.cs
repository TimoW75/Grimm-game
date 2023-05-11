using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrandmaDialogue : MonoBehaviour
{
    [SerializeField] private string[] lines;
    [SerializeField] private Sprite npcIcon;
    [SerializeField] private string GrandmaName;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textComponent;

    [SerializeField] private Image npcImageObject;
    [SerializeField] private TextMeshProUGUI npcName;
    private int index;
    private bool playerIsClose;
    void Start()
    {
        textComponent.text = string.Empty;
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
        npcImageObject.sprite = npcIcon;
        print(npcName.text);
        npcName.text = GrandmaName;
        print(npcName.text);
        index = 0;
        textComponent.text = lines[index];  

        NextLine();
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
            zeroText();
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        zeroText();
        playerIsClose = false;
    }
}
