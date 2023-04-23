using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    [SerializeField] private string questActiveText;

    [SerializeField] private float textSpeed;

    private int index;
    private bool playerIsClose;

    [SerializeField] private bool hasQuest;
    [SerializeField] private Item givenQuestItem;
    [SerializeField] private bool questActive;
    [SerializeField] private bool questCompeleted;
    [SerializeField] private bool itemReceived;
    [SerializeField] private bool hasReceivedClue;

    public InventoryManager inventoryManage;


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
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !questActive && !hasReceivedClue)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !questActive && hasReceivedClue)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
                hasReceivedClue = false;
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && questActive && !hasReceivedClue)
        {
            zeroText();
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
        if(!questActive && !questCompeleted)
        {
            textComponent.text = lines[index];
        }else if(questActive && !questCompeleted)
        {
            textComponent.text = questActiveText;
        }
        else if(questCompeleted && !questActive)
        {
            textComponent.text = "I have nothing to say to you";
        }
    }


    void NextLine()
    {
        if (hasQuest)
        {
            if (index < lines.Length - 1 && !questActive)
            {
                index++;
                textComponent.text = string.Empty;
                textComponent.text = lines[index];
            }
            else if(!questActive)
            {
                questActive = true;
                zeroText();
            }
            if (questActive && !questCompeleted)
            {
                zeroText();
            }
            if(questCompeleted && !questActive)
            {
                if (LieTruth.Length != 0)
                {
                    int responseIndex = Random.Range(0, 2);
                    print(responseIndex);
                    textComponent.text = LieTruth[responseIndex];
                    // here the player receives a clue
                    if (!itemReceived)
                    {
                        InventoryManager.Instance.AddItem(givenQuestItem);
                        inventoryManage.ListItems();
                        questCompeleted = true;
                    }
                }
                else
                {
                    if (!itemReceived)
                    {
                        InventoryManager.Instance.AddItem(givenQuestItem);
                        itemReceived = true;
                    }
                    else
                    {
                        zeroText();
                    }
                }
            }
        }
        else
        {
            if (index < lines.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                textComponent.text = lines[index];
            }
            else
            {
                if (LieTruth.Length != 0)
                {
                    int responseIndex = Random.Range(0, 2);
                    print(responseIndex);
                    textComponent.text = LieTruth[responseIndex];
                    if (!itemReceived)
                    {
                        InventoryManager.Instance.AddItem(givenQuestItem);
                        inventoryManage.ListItems();
                        questCompeleted = true;
                        questActive = false;
                        itemReceived = true;
                    }
                    hasReceivedClue = true;

                }
                else
                {
                    zeroText();
                }
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
