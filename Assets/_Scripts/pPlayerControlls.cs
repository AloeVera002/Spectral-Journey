using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class pPlayerControlls : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float rotationSpeed = 250f;
    [SerializeField] float jumpHeight = 4f;
    float gravity = -9.81f;

    private pPlayerComponent playerData;

    [SerializeField] float pebbleSpeed = 1800f;
    [SerializeField] GameObject pebblePlaceHolder;
    public float chargeTime;

    Vector2 movementInput;
    Vector3 movementVector;
    Vector3 velocity;

    CharacterController charController;

    public bool isSpeaking = false;
    public bool bCanJump = false;

    public bool canFire = false;

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

        if (playerData.isInConversation)
        {
            return;
        }
        charController.Move((movementVector + velocity) * Time.deltaTime);
        #endregion

        if (Input.GetMouseButton(0))
        {
            /*if (chargeTime >= 1)
            { 
                pebblePlaceHolder.SetActive(true);
                canFire = true;
                return;
            }
            else
            {
                canFire = false;
            }
            chargeTime += Time.deltaTime;*/
            pebblePlaceHolder.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if (!canFire) return;
            if (playerData.pebbleCount > 0)
            {
                canFire = true;
                FireSlingShot();
                Debug.Log("Bla");
            }
            else
            {
                ResetSlingshot();
                playerData.ActivateNoPebbleText();
            }
            playerData.UpdatePebbleText();
            //chargeTime = 0;
        }

        if (movementVector != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
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
        if (playerData.isInConversation) { return; }

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
        pebblePlaceHolder.SetActive(false);
        GameObject newPebble = Instantiate(playerData.pebblePrefab, playerData.slingshotPivot.position, playerData.slingshotPivot.rotation);
        newPebble.AddComponent<Rigidbody>();

        newPebble.GetComponent<Rigidbody>().AddForce(transform.forward * pebbleSpeed);
        newPebble.GetComponent<PebbleController>().aimPos = playerData.slingshotPivot.position;
        newPebble.tag = "Pebble";

        playerData.DecreasePebbleCount(1);
    }

    void ResetSlingshot()
    {
        playerData.SetPebbleCount(0);

    }
    #endregion
}