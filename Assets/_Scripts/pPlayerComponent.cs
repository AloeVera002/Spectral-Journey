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


    [SerializeField] CinemachineCamera NormalCam, DialogueCam;
    
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
        }
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
        NormalCam.Lens.FieldOfView = newPOV;

        DialogueCam.Lens.FieldOfView = dialoguePOV;
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