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

    QuestManager questMan;
    
    [SerializeField] bool isQuestioned;
    bool isCameraswitched;

    private dialogueLine[] lineArray;
    int lineIndex = 0;
    bool inProgress = false;

    public delegate void DialogueStarted();
    public event DialogueStarted OnDialogueStarted;

    void Start()
    {
        npcSpeakerBubble = dialogueField.transform.GetChild(1).gameObject;
        playerSpeakerBubble = dialogueField.transform.GetChild(2).gameObject;
        Debug.Log("NPC speaker: " + dialogueField.transform.GetChild(1).gameObject.name +
            " PlayerSpeaker: " + dialogueField.transform.GetChild(2).gameObject.name);
        GetInfo();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (!GetComponent<pPlayerComponent>().canInteract) return;
            if (GetComponent<pPlayerComponent>().isInConversation) return;

            StartNewDialogue();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!GetComponent<pPlayerComponent>().isInConversation) return;

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
    {
        Debug.Log("started dialogue");
        lineIndex = 0;
        dialogueField.SetActive(true);

        GetComponent<pPlayerComponent>().ToggleDialogueCamera();

        GetComponent<pPlayerComponent>().isInConversation = true;

        GetComponent<QuestManager>().HideQuestHUD();

        UpdateTextInput(lineArray[lineIndex].Name, lineArray[lineIndex].text);

        /*
        StartCoroutine(TypeLine());
        UpdateTextInput(textArray[speechIndex].Name, textArray[speechIndex].text);*/
    }

    private void UpdateTextInput(string newSpeaker, string newText)
    {
        if (newSpeaker == "Player")
        {
            speaker = playerSpeakerBubble.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
            playerSpeakerBubble.SetActive(true);
            npcSpeakerBubble.SetActive(false);
            speaker.color = Color.blue;
        }
        else
        {
            speaker = npcSpeakerBubble.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
            npcSpeakerBubble.SetActive(true);
            playerSpeakerBubble.SetActive(false);
            speaker.color = Color.yellow;
        }

        speaker.text = newSpeaker;
        //    string finalOutput = ReplacePlaceholderText(newText, "{i}", lineIndex.ToString());
        line.text = newText; // finalOutput;
        GetComponent<pPlayerComponent>().UpdateText(GetComponent<pPlayerComponent>().ectroplasmText, GetComponent<pPlayerComponent>().ectoplasm.ToString());
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
            oppositeTalker.GetComponent<QuestGiver>().hasQuest = false;
            QuitDialogue();
        }
        if (lineArray[lineIndex].isReward)
        {
            GetComponent<QuestManager>().GiveQuestReward();
        }
    }

    public void QuitDialogue()
    {
        dialogueField.SetActive(false);

        GetComponent<pPlayerComponent>().ToggleDialogueCamera();
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
        GetComponent<QuestManager>().StartQuest();
    }

    public void AcceptQuestion()
    {
        ShowModal();
        SetDialogueRef(lineArray[lineIndex].nextDialogue);
        UpdateDialogue();
        oppositeTalker.GetComponent<QuestGiver>().hasQuest = true;
    }

    public void RejectQuestion()
    {
        if (!lineArray[lineArray.Length].nextDialogue)
        {
            QuitDialogue();
            return;
        }

        ShowModal();
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
