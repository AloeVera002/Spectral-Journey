using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class pPlayerControlls : MonoBehaviour
{
    float movementSpeed = 5;
    float rotationSpeed = 250;
    Vector2 movementInput;
    Vector3 movementVector;
    CharacterController charController;

    public bool isSpeaking = false;

    public CinemachineFollow cineFollow;
    public CinemachineCamera cam;

    int normPOV = 40;
    [SerializeField] int dialoguePOV = 12;
    [SerializeField] Vector3 normVector;
    [SerializeField] Vector3 dialogueVector;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        charController.Move((movementVector) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Testing();
        }
    }

    #region Basic Movement
    public void Move(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>();
        movementVector = new Vector3(movementInput.x * movementSpeed, 0, movementInput.y * movementSpeed);
        HandleCharacterRotation();
    }

    public void Jump()
    {

    }

    #region Additional Basic Movement stuff
    void HandleCharacterRotation()
    {
        if (movementVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    #endregion
    #endregion

    #region Interactable Actions
    void Interact()
    {

    }
    #endregion

    #region Camera

    void Testing()
    {
        switchBool(isSpeaking);
        ToggleDialogueCamera();
    }

    void ToggleDialogueCamera()
    {
        if (!isSpeaking)
        {
            UpdateCineFollow(dialogueVector, dialoguePOV);
        }
        else
        {
            UpdateCineFollow(normVector, normPOV);
        }
    }

    void UpdateCineFollow(Vector3 pos, int fov)
    {
        cineFollow.FollowOffset = pos;
        cam.Lens.FieldOfView = fov;
    }
    #endregion

    bool switchBool(bool boolToSwitch)
    {
        boolToSwitch = !boolToSwitch;
        return boolToSwitch;
    }
}