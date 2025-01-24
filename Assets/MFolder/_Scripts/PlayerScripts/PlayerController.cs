using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRotationSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpButtonGracePeriod;

    private CharacterController characterController;
    private float ySpeed;
    private float originaleStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originaleStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originaleStepOffset;
            ySpeed = -0.5f;
            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
               ySpeed = jumpSpeed;
               jumpButtonPressedTime = null;
               lastGroundedTime = null;
            }
        }
        else 
        {
            characterController.stepOffset = 0;
        }


        Vector3 velocity = movementDirection * playerSpeed;
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, playerRotationSpeed * Time.deltaTime);
        }
    }
}
