using TMPro;
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

    private pPlayerComponent playerData;

    [SerializeField] float pebbleSpeed = 1800f;

    Vector2 movementInput;
    Vector3 movementVector;
    Vector3 velocity;

    CharacterController charController;

    public bool isSpeaking = false;
    public bool bCanJump = false;

    public bool canFire = false;
    public bool isEquipped = false;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        playerData = GetComponent<pPlayerComponent>();
    }

    void Update()
    {
        #region Movement
        bool bIsGrounded = charController.isGrounded;

        if (bIsGrounded)
        {
            bCanJump = true;
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }
        else
        {
            bCanJump = false;
        }

        velocity.y += gravity * Time.deltaTime;

        charController.Move((movementVector + velocity) * Time.deltaTime);
        #endregion

        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleSlingshot();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (playerData.pebbleCount > 0 && isEquipped)
            {
                FireSlingShot();
                Debug.Log("Bla");
            }
            else
            {
                ResetSlingshot();
                if (isEquipped)
                {
                    playerData.ActivateNoPebbleText();
                }
            }
            playerData.UpdatePebbleText();
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

    #region Slingshot
    void FireSlingShot()
    {
        GameObject newPebble = Instantiate(playerData.pebblePrefab, playerData.slingshotPivot.position, playerData.slingshotPivot.rotation);
        newPebble.AddComponent<Rigidbody>();

        newPebble.GetComponent<Rigidbody>().AddForce(transform.forward * pebbleSpeed);
        newPebble.GetComponent<TestingPebbleShootingMechanic>().aimPos = playerData.slingshotPivot.position;
        newPebble.tag = "Pebble";

        playerData.DecreasePebbleCount(1);
    }

    void ToggleSlingshot()
    {
        if (!isEquipped)
        {
            playerData.slingshot.SetActive(true);
            canFire = true;
        }
        else
        {
            playerData.slingshot.SetActive(false);
            canFire = false;
        }
        isEquipped = !isEquipped;
    }

    void ResetSlingshot()
    {
        playerData.SetPebbleCount(0);

    }
    #endregion
}