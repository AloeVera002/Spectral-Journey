using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class pPlayerControlls : MonoBehaviour
{
    float movementSpeed = 6.5f;
    float rotationSpeed = 250f;
    [SerializeField] float jumpHeight = 5f;
    float gravity = -9.81f;

    Vector2 movementInput;
    Vector3 movementVector;
    Vector3 velocity;

    CharacterController charController;

    public bool isSpeaking = false;
    public bool bCanJump = false;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool bIsGrounded = charController.isGrounded;

        if (bIsGrounded)
        {
            bCanJump = true;
        }
        else
        {
            bCanJump = false;
        }

        if (bIsGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        charController.Move((movementVector + velocity) * Time.deltaTime);
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Testing();
        }*/
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
        if (bCanJump)
        {
            velocity.y = jumpHeight;
        }
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
}