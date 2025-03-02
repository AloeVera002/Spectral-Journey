using JetBrains.Annotations;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueField;
    GameObject npcSpeakerBubble, playerSpeakerBubble;
    [SerializeField] GameObject modalButtons;
    [SerializeField] so_Dialogue dialogueRef;
    [SerializeField] TMP_Text speaker, line;
    [SerializeField] float textSpeed;
    [SerializeField] public GameObject oppositeTalker;

    QuestManager questManDia;
    pPlayerComponent playerCompDia;


    [SerializeField] bool isQuestioned;
    bool isCameraswitched;

    private dialogueLine[] lineArray;
    public int lineIndex = 0;
    bool inProgress = false;

    public delegate void DialogueStarted();
    public event DialogueStarted OnDialogueStarted;

    void Start()
    {
        npcSpeakerBubble = dialogueField.transform.GetChild(1).gameObject;
        playerSpeakerBubble = dialogueField.transform.GetChild(2).gameObject;
        Debug.Log("NPC speaker: " + dialogueField.transform.GetChild(1).gameObject.name +
            " PlayerSpeaker: " + dialogueField.transform.GetChild(2).gameObject.name);
    //    GetInfo();

        questManDia = GetComponent<QuestManager>();
        playerCompDia = GetComponent<pPlayerComponent>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (!playerCompDia.canInteract || playerCompDia.isInConversation) return;

            if (playerCompDia.canInteract)
            {
                StartNewDialogue();
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!playerCompDia.isInConversation) return;

            if (lineArray[lineIndex].isQuestion)
            {/*
                if (modalButtons.activeInHierarchy == false)
                {
                    ShowModal();
                }
                else
                {*/
                Debug.Log("isQuestion should be: " + lineArray[lineIndex].isQuestion);
                AcceptQuestion();
                //    }
            }
            else if (lineArray[lineIndex].isGiveQuest)
            {
                Debug.Log("isGiveQuest should be: " + lineArray[lineIndex].isGiveQuest);
                AcceptQuest();
            }
            else
            {
                Debug.Log("isQuestion and isGiveQuest should be (false): " + "d: " + lineArray[lineIndex].isQuestion + "q: " + lineArray[lineIndex].isGiveQuest);
                NextLine();
            }
        }
    }

    void GetInfo()
    {
        lineArray = dialogueRef.dialogue;
        Debug.Log("textArraySize: " + lineArray.Length);
    }

    public void StartDialogue()
    {/*
        if (GetComponent<QuestManager>().questRef != oppositeTalker.GetComponent<QuestGiver>().questToGive[oppositeTalker.GetComponent<QuestGiver>().questIndex]) //GetComponent<QuestManager>().ongoingQuest && 
        {

        }
        else
        {*/
            Debug.Log("started dialogue");

            lineIndex = 0;
            dialogueField.SetActive(true);

            playerCompDia.isInConversation = true;
            Debug.Log("Is in convo: " + playerCompDia.isInConversation);

            questManDia.HideQuestHUD();
            playerCompDia.ToggleDialogueCamera();

            UpdateTextInput(lineArray[lineIndex].Name, lineArray[lineIndex].text);

            /*
            StartCoroutine(TypeLine());
            UpdateTextInput(textArray[speechIndex].Name, textArray[speechIndex].text);*/
    //    }
    }

    private void UpdateTextInput(string newSpeaker, string newText)
    {
        if (newSpeaker == "{p}")
        {
            newSpeaker = newSpeaker.Replace("{p}", GetComponent<pPlayerComponent>().GetPlayerName());
            speaker = playerSpeakerBubble.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
            playerSpeakerBubble.SetActive(true);
            npcSpeakerBubble.SetActive(false);
            speaker.color = Color.yellow;
        }
        else
        {
            speaker = npcSpeakerBubble.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
            npcSpeakerBubble.SetActive(true);
            playerSpeakerBubble.SetActive(false);
            speaker.color = Color.blue;
        }

        speaker.text = newSpeaker;
        //    string finalOutput = ReplacePlaceholderText(newText, "{i}", lineIndex.ToString());
        line.text = newText; // finalOutput;
    }

    public string ReplacePlaceholderText(string uneditedText, string replaceText, string editedText)
    {
        uneditedText = uneditedText.Replace(replaceText, editedText);

        return uneditedText;
    }

    private void UpdateText(string newSpeaker)
    {
        speaker.text = newSpeaker;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        inProgress = true;
        foreach (char letter in lineArray[lineIndex].text.ToCharArray())
        {
            line.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void FinishLine(string finishLine)
    {
        StopCoroutine(TypeLine());
        line.text = finishLine;
        inProgress = false;
    }

    void NextLine()
    {
        if (lineIndex < lineArray.Length - 1)
        {
            lineIndex += 1;
            UpdateTextInput(lineArray[lineIndex].Name, lineArray[lineIndex].text);
        }
        else
        {
            oppositeTalker.GetComponent<QuestGiver>().dontGiveNewDialogue = false;
            QuitDialogue();
        }
        if (lineArray[lineIndex].isReward)
        {
            questManDia.GiveQuestReward();
        }
    }

    public void QuitDialogue()
    {
        dialogueField.SetActive(false);

        playerCompDia.isInConversation = false;
        playerCompDia.ToggleDialogueCamera();

        questManDia.TutorialScreenPopUp();
    }

    void ShowModal()
    {
        if (!isQuestioned)
        {
            modalButtons.SetActive(true);
        }
        else
        {
            modalButtons.SetActive(false);
        }
        isQuestioned = !isQuestioned;
    }

    public void AcceptQuest()
    {
        QuitDialogue();
        if (!questManDia.ongoingQuest)
        {
            questManDia.StartQuest();
        }
        else
        {
            Debug.Log("already has quest");
        }
    }

    public void AcceptQuestion()
    {
    //    ShowModal();
        SetDialogueRef(lineArray[lineIndex].nextDialogue);
        UpdateDialogue();
        oppositeTalker.GetComponent<QuestGiver>().dontGiveNewDialogue = true;
    }

    public void RejectQuestion()
    {
        if (!lineArray[lineArray.Length].nextDialogue)
        {
            QuitDialogue();
            return;
        }

    //    ShowModal();
        SetDialogueRef(lineArray[lineIndex].nextDialogue);
        UpdateDialogue();
    }

    public void StartNewDialogue()
    {
        GetInfo();
        StartDialogue();
    }

    public void SetDialogueRef(so_Dialogue newDialogue)
    {
        Debug.Log("Set Dialogue ref to " + newDialogue);
        dialogueRef = newDialogue;
    }

    public void UpdateDialogue()
    {
        Debug.Log(lineIndex);
        
        StartNewDialogue();
    }
}
