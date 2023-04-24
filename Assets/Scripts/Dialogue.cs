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
    [SerializeField] private string[] LieTruth;
    [SerializeField] private string questActiveText = "You are already doing my quest!";
    [SerializeField] private string[] HasQuestItemText;

    [SerializeField] private float textSpeed;

    private int index;
    private bool playerIsClose;

    [Header("NPC Quest")]
    [SerializeField] private bool hasQuest;
    [SerializeField] private string ItemNeededForQuest;
    [SerializeField] private string ItemNeededInInvToCompleteQuestName;
    [SerializeField] private Item givenQuestItem;
    private bool questActive;
    private bool questCompeleted;
    private bool itemReceived;
    private bool hasReceivedClue;

    public InventoryManager inventoryManage;

    public PlayerChopping playerChop;
    [SerializeField] private GameObject SubmitQuestItemSlot;
    [SerializeField] private GameObject inventory;

    public questManager manageQuests;
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
        }else if (Input.GetKeyDown(KeyCode.Return) && playerIsClose && questActive && !hasReceivedClue && !questCompeleted)
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

        if (manageQuests.questActiveGeneral && !questActive)
        {
            textComponent.text = "You are already doing a quest";
        }
        if (!questActive && !questCompeleted && !manageQuests.questActiveGeneral)
        {
            textComponent.text = lines[index];

        }else if(questActive && !questCompeleted)
        {
            if (inventory != null && SubmitQuestItemSlot != null)
            {
                inventory.SetActive(true);
                inventoryManage.ListItems();
            }
            if (SubmitQuestItemSlot != null)
            {
                SubmitQuestItemSlot.SetActive(true);
                if(SubmitQuestItemSlot.transform.GetChild(0).transform.childCount != 0)
                {
                    print(SubmitQuestItemSlot.transform.GetChild(0).transform.GetChild(0).name);
                    GameObject.Destroy(SubmitQuestItemSlot.transform.GetChild(0).transform.GetChild(0).gameObject);    
                }   
                textComponent.text = HasQuestItemText[index];
            }
            else
            {
                textComponent.text = questActiveText;
            }

        }
        else if(questCompeleted && questActive && hasReceivedClue)
        {
            textComponent.text = "You have already completed my quest!";
        }
    }


    void NextLine()
    {
        if (hasQuest)
        {

            if (index < lines.Length - 1 && !questActive && !manageQuests.questActiveGeneral)
            {
                index++;
                textComponent.text = string.Empty;
                textComponent.text = lines[index];
            }
            else if(!questActive)
            {
                questActive = true;
                manageQuests.questActiveGeneral = true;

                if (ItemNeededForQuest == "Axe" && playerChop != null)
                {
                    playerChop.setActiveAxe();
                }
                zeroText();
            }
            if (questActive && !questCompeleted)
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
                        else if(!hasReceivedClue)
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
                                    itemReceived = true;
                                    hasReceivedClue = true;

                                }
                                manageQuests.questActiveGeneral = false;

                            }
                            else
                            {
                                if (!itemReceived)
                                {
                                    InventoryManager.Instance.AddItem(givenQuestItem);
                                    itemReceived = true;
                                    questCompeleted = true;
                                }
                                else
                                {
                                    zeroText();
                                }
                                manageQuests.questActiveGeneral = false;
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
            if(questCompeleted && !questActive)
            {
                textComponent.text = "You have already completed my quest!";

            }
        }
        else if(!hasQuest && !manageQuests.questActiveGeneral)
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
                    manageQuests.questActiveGeneral = false;

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
            zeroText();
        }
    }
}
