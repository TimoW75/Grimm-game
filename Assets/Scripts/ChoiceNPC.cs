using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ChoiceNPC : MonoBehaviour
{
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject[] Field;
    [SerializeField] private string[] rightWords;
    public InventoryManager inventoryM;


    [Header("NPC Dialogue Box")]
    [SerializeField] private Sprite npcIcon;
    [SerializeField] private string NPCname;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textComponent;

    [SerializeField] private Image npcImageObject;
    [SerializeField] private TextMeshProUGUI npcName;

    [SerializeField] private string[] lines;
    [SerializeField] private string questActiveText = "You are already doing my quest!";
    [SerializeField] private string[] HasQuestItemText;

    [SerializeField] private float textSpeed;

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
    private bool hasReceivedClue;

    public InventoryManager inventoryManage;

    public PlayerChopping playerChop;
    [SerializeField] private GameObject SubmitQuestItemSlot;

    [SerializeField] private int QuestActiveOnDay;
    public gameManager gameManager;


    private bool field0;
    private bool field1;
    private bool field2;
    private bool field3;
    private bool field4;
    private bool field5;
    private bool field6;
    private bool field7;
    private bool field8;
    private bool field9;
    private bool field10;

    private int numberCorrect = 0;
    void Start()
    {
        choicePanel.SetActive(false);
    }
    private void Update()
    {
        if(gameManager.dayNumber == 3 && !questCompeleted)
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
            else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
            {
                choicePanel.SetActive(false);
                inventory.SetActive(false);
                checkFilledFields();
                DestroyImageChildrenWithTag();
                playerIsClose = false;
                dialoguePanel.SetActive(false);
                zeroText();
            }
            if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !questActive && !hasReceivedClue)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    NextLine();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && !questActive && hasReceivedClue)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    zeroText();
                    hasReceivedClue = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && questActive && !hasReceivedClue && !questCompeleted)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    NextLine();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && questActive && hasReceivedClue && questCompeleted)
            {
                if (dialoguePanel.activeInHierarchy)
                {
                    NextLine();
                }
            }

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F) && playerIsClose)
            {
                if (choicePanel.activeInHierarchy)
                {
                    choicePanel.SetActive(false);
                    inventory.SetActive(false);
                    DestroyImageChildrenWithTag();
                }
                else
                {
                    choicePanel.SetActive(true);
                    inventory.SetActive(true);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose)
            {
                choicePanel.SetActive(false);
                inventory.SetActive(false);
                checkFilledFields();
                DestroyImageChildrenWithTag();
                playerIsClose = false;
                dialoguePanel.SetActive(false);
                zeroText();
            }
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
        choicePanel.SetActive(false);
        inventory.SetActive(false);
        checkFilledFields();
        DestroyImageChildrenWithTag();

        if (collision.CompareTag("Player"))
        {
            playerIsClose = false;
            dialoguePanel.SetActive(false);
            zeroText();
        }
    }

    void DestroyImageChildrenWithTag()
    {
        for (int i = 0; i < Field.Length; i++)
        {
            foreach (Transform child in Field[i].transform)
            {
                Destroy(child.gameObject);
                
            }
        }
        inventoryM.ListItems();
    }

    void checkFilledFields()
    {
        for (int i = 0; i < Field.Length; i++)
        {   
            if (Field[i].gameObject.transform.childCount != 0)
            {
                if (Field[i].gameObject.transform.GetChild(0).name == rightWords[i])
                {
                    numberCorrect++;
                    print("number " + i + " is right");
                }
                else
                {
                    print("number " + i + " is wrong");
                }
            }
        }
        print(numberCorrect);
        if (numberCorrect == 4)
        {
            gameManager.dayNumber++;
        }else if (numberCorrect == 7)
        {
            gameManager.dayNumber++;
        }else if(numberCorrect == 10)
        {
            gameManager.dayNumber++;
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
                textComponent.text = "You are already doing a quest";
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
            else if (questCompeleted && questActive && hasReceivedClue)
            {
                textComponent.text = "You have already completed my quest!";
            }
        }
        else
        {
            textComponent.text = "Come back tomorrow";
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
            else if (questCompeleted && questActive && itemReceived && hasReceivedClue)
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
                        else if (!hasReceivedClue)
                        {

                            if (!itemReceived)
                            {
                                for (int i = 0; i < givenQuestItem.Length; i++)
                                {
                                    InventoryManager.Instance.AddItem(givenQuestItem[i]);
                                }
                                itemReceived = true;
                                questCompeleted = true;
                            }
                            else
                            {
                                zeroText();
                            }
                            hasReceivedClue = true;
                            gameManager.questActiveGeneral = false;
                            gameManager.currentQuest = string.Empty;
                            gameManager.setTextHiden();

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

}
