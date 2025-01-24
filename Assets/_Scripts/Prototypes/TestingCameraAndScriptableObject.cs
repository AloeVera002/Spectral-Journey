using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class TestingCameraAndScriptableObject : MonoBehaviour
{
    public Vector3 CameraOffsetPos;
    public Vector3 oldPos, oldRot;
    public Vector3 newPos;
    public int newCamFOV;

    public CinemachineFollow cineFollow;
    public CinemachineCamera cam;

    bool switchedCamera = false;

    [SerializeField] TestForScriptableObjectDialogue table;
    [SerializeField] TMP_Text speaker, speech;

    private dialogueLine[] textArray;

    int speechIndex = 0;

    void Start()
    {
        GetInfo();
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            switchBool();
            UpdateCameraPosNRot();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GoToNextLine();
        }
    }

    void UpdateCameraPosNRot()
    {
        if (!switchedCamera)
        {
            UpdateCineFollow(newPos, newCamFOV);
        }
        else
        {
            UpdateCineFollow(oldPos, 40);
        }
    }

    bool switchBool()
    {
        switchedCamera = !switchedCamera;
        Debug.Log("Switched Camera: " + switchedCamera);
        return switchedCamera;
    }

    void UpdateCineFollow(Vector3 pos, int fov)
    {
        cineFollow.FollowOffset = pos;
        cam.Lens.FieldOfView = fov;
    }

    #region Dialogue ScriptableObj
    void GetInfo()
    {
        textArray = table.dialogue;
        Debug.Log("textArraySize: " + textArray.Length);
    }

    public void StartDialogue()
    {
        UpdateTextInput(textArray[speechIndex].Name, textArray[speechIndex].text);
    }

    void GoToNextLine()
    {
        Debug.Log("called GoToNextLine");

        UpdateIndex();
        UpdateTextInput(textArray[speechIndex].Name, textArray[speechIndex].text);
    }

    void UpdateIndex()
    {
        speechIndex = SetIndex(speechIndex, textArray.Length - 1);
    }

    int SetIndex(int oldIndex, int maxIndex)
    {
        int newIndex = 0;
        if (oldIndex < maxIndex)
        {
            newIndex = oldIndex += 1;
        }
        else
        {
            newIndex = 0;
        }
        Debug.Log("SetIndex: " + newIndex);
        return newIndex;
    }

    private void UpdateTextInput(string newSpeaker, string newText)
    {
        Debug.Log("Heilo called UpdateTextInput");
        speaker.text = newSpeaker;
        speech.text = newText;
    }

    #endregion Dialogue ScriptableObj
}
