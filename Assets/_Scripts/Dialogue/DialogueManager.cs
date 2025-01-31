using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueField;
    [SerializeField] GameObject modalButtons;
    [SerializeField] so_Dialogue dialogueRef;
    [SerializeField] TMP_Text speaker, line;
    [SerializeField] float textSpeed;

    [SerializeField] bool isQuestioned;

    private dialogueLine[] lineArray;
    int lineIndex = 0;
    bool inProgress = false;

    void Start()
    {
        GetInfo();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            if (GetComponent<pPlayerComponent>().isInConversation) return;
            StartDialogue();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!GetComponent<pPlayerComponent>().isInConversation) return;
            /*
            if (inProgress)
            {
                FinishLine(lineArray[lineIndex].text);
            }
            else
            {
                NextLine();
            }*/

            if (lineArray[lineIndex].isQuestion)
            {
                ShowModal();
            }
            else
            {
                if (lineArray[lineIndex].isQuestion)
                {
                    UpdateDialogue();
                }
                else
                {
                    NextLine();
                }
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

        GetComponent<pPlayerComponent>().isInConversation = true;

        UpdateTextInput(lineArray[lineIndex].Name, lineArray[lineIndex].text);
        //       UpdateText(lineArray[lineIndex].Name);

        /*
        StartCoroutine(TypeLine());
        UpdateTextInput(textArray[speechIndex].Name, textArray[speechIndex].text);*/
    }

    private void UpdateTextInput(string newSpeaker, string newText)
    {
        speaker.text = newSpeaker;
        line.text = newText;
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
            //    line.text = string.Empty;
            //    UpdateText(lineArray[lineIndex].Name);
            UpdateTextInput(lineArray[lineIndex].Name, lineArray[lineIndex].text);
        }
        else
        {
            QuitDialogue();
        }
    }

    public void QuitDialogue()
    {
        dialogueField.SetActive(false);

        if (!dialogueField.activeInHierarchy) GetComponent<pPlayerComponent>().isInConversation = false;
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
        ShowModal();
     //   SetDialogueRef(lineArray[lineIndex].AcceptedDialogue);
    //    UpdateDialogue();
        QuitDialogue();
        GetComponent<QuestManager>().StartQuest();
    }

    public void RejectQuest()
    {
        ShowModal();
        SetDialogueRef(lineArray[lineIndex].AcceptedDialogue);
        UpdateDialogue();
    }

    public void StartNewDialogue()
    {
        GetInfo();
        StartDialogue();
    }

    public void SetDialogueRef(so_Dialogue newQuest)
    {
        Debug.Log("Set Dialogue ref to " + newQuest);
        dialogueRef = newQuest;
    }

    public void UpdateDialogue()
    {
        Debug.Log(lineIndex);
        
        StartNewDialogue();
    }
}
