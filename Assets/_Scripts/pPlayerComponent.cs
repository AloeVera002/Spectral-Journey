using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pPlayerComponent : MonoBehaviour
{
    [SerializeField] public bool isInteracting = false;
    [SerializeField] public bool isInConversation = false;

    Animator animator;

    [SerializeField] int normPOV = 60;
    [SerializeField] int dialoguePOV = 12;
    [SerializeField] Vector3 normVector;
    [SerializeField] Vector3 dialogueVector;

    [SerializeField] TMP_Text ectroplasmText;
    [SerializeField] GameObject NormalCam, DialogueCam;
    
    public int ectoplasm;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateCameraData(normVector, normPOV, dialogueVector, dialoguePOV);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            UpdateCameraData(normVector, normPOV, dialogueVector, dialoguePOV);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            SwitchCamera();
            ectoplasm++;
        }

        ectroplasmText.text = ectoplasm.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            WaterDeath();
        }
    }

    void UpdateCameraData(Vector3 newOffset, int newPOV, Vector3 newDialogueOffset, int newDialoguePOV)
    {
        NormalCam.GetComponent<CinemachineFollow>().FollowOffset = newOffset;
        NormalCam.GetComponent<CinemachineCamera>().Lens.FieldOfView = newPOV;

        DialogueCam.GetComponent<CinemachineCamera>().Lens.FieldOfView = dialoguePOV;
    }

    void SwitchCamera()
    {
        if (isInConversation)
        {
            animator.Play("GhostCamera");
        }
        else
        {
            animator.Play("FollowCamera");
        }
        isInConversation = !isInConversation;
    }

    void WaterDeath()
    {
        SceneManager.LoadScene(0);
    }
}