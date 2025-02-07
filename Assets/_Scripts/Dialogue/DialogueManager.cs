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
    bool isCameraswitched;

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
            //    UpdateDialogue();
            }
            else
            {
                if (lineArray[lineIndex].giveQuest)
                {
                    AcceptQuest();
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

        if (!isCameraswitched)
        {
            //GetComponent<pPlayerComponent>().SwitchCamera();
        }

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

        //GetComponent<pPlayerComponent>().SwitchCamera();
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
    //    ShowModal();
    //   SetDialogueRef(lineArray[lineIndex].AcceptedDialogue);
    //    UpdateDialogue();
        QuitDialogue();
        GetComponent<QuestManager>().StartQuest();
    }

    public void AcceptQuestion()
    {
        ShowModal();
        SetDialogueRef(lineArray[lineIndex].AcceptedDialogue);
        UpdateDialogue();
    }

    public void RejectQuestion()
    {
        if (!lineArray[lineArray.Length -1].RejectedDialogue)
        {
            QuitDialogue();
            return;
        }

        ShowModal();
        SetDialogueRef(lineArray[lineIndex].RejectedDialogue);
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
