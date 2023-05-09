using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    [Header("NPC Dialogue Box")]
    [SerializeField] private Sprite npcIcon;
    [SerializeField] private string NPCname;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textComponent;

    [SerializeField] private Image npcImageObject;
    [SerializeField] private TextMeshProUGUI npcName;

    [SerializeField] private string[] lines;
    [SerializeField] private string questActiveText;
    [SerializeField] private string[] HasQuestItemText;

    private int index;
    private bool playerIsClose;

    [Header("NPC Quest")]
    [SerializeField] private bool hasQuest;
    [SerializeField] private string ItemNeededForQuest;
    [SerializeField] private string ItemNeededInInvToCompleteQuestName;
    [SerializeField] private Item[] givenQuestItem;
    private bool questActive;
    private bool questCompeleted;
    private bool itemReceived;

    public InventoryManager inventoryManage;

    public PlayerChopping playerChop;
    [SerializeField] private GameObject SubmitQuestItemSlot;
    [SerializeField] private GameObject inventory;

    [SerializeField] private int QuestActiveOnDay;

    public gameManager gameManager;
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
        if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !questActive )
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !questActive)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && questActive && !questCompeleted)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                NextLine();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && questActive && questCompeleted)
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
        if (SubmitQuestItemSlot != null)
        {
            SubmitQuestItemSlot.SetActive(false);
        }
        if (inventory != null)
        {
            inventory.SetActive(false);
        }
    }

    void StartDialgue()
    {
        npcImageObject.sprite = npcIcon;
        npcName.text = NPCname;
        index = 0;

        if (QuestActiveOnDay == gameManager.dayNumber)
        {
            if (gameManager.questActiveGeneral && !questActive)
            {
                textComponent.text = questActiveText;
            }
            else if (!questActive && !questCompeleted && !gameManager.questActiveGeneral)
            {
                textComponent.text = lines[index];

            }
            else if (questActive && !questCompeleted)
            {
                if (inventory != null && SubmitQuestItemSlot != null)
                {
                    inventory.SetActive(true);
                    inventoryManage.ListItems();
                }
                if (SubmitQuestItemSlot != null)
                {
                    SubmitQuestItemSlot.SetActive(true);
                    if (SubmitQuestItemSlot.transform.GetChild(0).transform.childCount != 0)
                    {
                        GameObject.Destroy(SubmitQuestItemSlot.transform.GetChild(0).transform.GetChild(0).gameObject);
                    }
                    textComponent.text = HasQuestItemText[index];
                }
                else
                {
                    textComponent.text = questActiveText;
                }

            }
            else if (questCompeleted && questActive)
            {
                textComponent.text = "I already told you what you wanted to know";
            }
        }
        else
        {
            textComponent.text = "I'm sorry Red, i can't talk today";
        }
    }


    void NextLine()
    {
        if (hasQuest && QuestActiveOnDay == gameManager.dayNumber)
        {

            if (index < lines.Length - 1 && !questActive && !gameManager.questActiveGeneral)
            {
                index++;
                textComponent.text = string.Empty;
                textComponent.text = lines[index];
            }
            else if (questCompeleted && questActive && itemReceived)
            {
                zeroText();
            }
            else if (!questActive && !questCompeleted)
            {
                questActive = true;
                gameManager.questActiveGeneral = true;
                gameManager.currentQuest = gameObject.name;
                gameManager.setTextActive();

                if (ItemNeededForQuest == "Axe" && playerChop != null)
                {
                    playerChop.setActiveAxe();
                }

                zeroText();
            }
            else if (questActive && !questCompeleted)
            {
                if (SubmitQuestItemSlot.transform.GetChild(0).transform.childCount != 0)
                {
                    if (SubmitQuestItemSlot.transform.GetChild(0).transform.GetChild(0).name == ItemNeededInInvToCompleteQuestName)
                    {
                        if (index < HasQuestItemText.Length - 1)
                        {
                            index++;
                            textComponent.text = string.Empty;
                            textComponent.text = HasQuestItemText[index];
                        }
                        else if (!itemReceived)
                        {
                            for (int i = 0; i < givenQuestItem.Length; i++)
                            {
                                InventoryManager.Instance.AddItem(givenQuestItem[i]);
                            }
                            itemReceived = true;
                            questCompeleted = true;
                            gameManager.questActiveGeneral = false;
                            gameManager.currentQuest = string.Empty;
                            gameManager.setTextHiden();

                            for (int i = inventoryManage.items.Count - 1; i >= 0; i--)
                            {
                                if (inventoryManage.items[i].name == ItemNeededInInvToCompleteQuestName)
                                {
                                    inventoryManage.items.RemoveAt(i);
                                }
                            }

                        }
                        else
                        { 
                            zeroText();
                        }
                    }
                    else
                    {
                        textComponent.text = "I have not received the item I asked for yet";
                    }

                }
            }
        }
        else if (!hasQuest && !gameManager.questActiveGeneral && QuestActiveOnDay == gameManager.dayNumber)
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
                
            }
        }
        else if (QuestActiveOnDay != gameManager.dayNumber)
        {
            zeroText();
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
            zeroText();
        }
    }
}