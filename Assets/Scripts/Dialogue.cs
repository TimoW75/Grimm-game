using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour
{
    [SerializeField] private Sprite npcIcon;
    [SerializeField] private string NPCname;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textComponent;

    [SerializeField] private Image npcImageObject;
    [SerializeField] private TextMeshProUGUI npcName;

    [SerializeField] private string[] lines;
    [SerializeField] private string[] LieTruth;

    [SerializeField] private float textSpeed;

    private int index;
    private bool playerIsClose;
    private bool hasReceivedClue;

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
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !hasReceivedClue)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && hasReceivedClue)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
                hasReceivedClue = false;
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
        npcName.text = NPCname;
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
            if(LieTruth.Length != 0)
            {
                int responseIndex = Random.Range(0, 2);
                print(responseIndex);
                textComponent.text = LieTruth[responseIndex];
                hasReceivedClue = true;
            }
            else
            {
                zeroText();
            }
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
