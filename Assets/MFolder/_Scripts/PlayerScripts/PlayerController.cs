using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;
using TMPro;
using System.ComponentModel;
using System.Collections;

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
    
    public Transform slingshotPivot;
    public GameObject slingshot;
    public GameObject pebblePrefab;
    public float pebbleSpeed = 500f;

    public bool pebbleInstantiated = false;

    public int pebbleCount = 0;
    public TMP_Text pebbleCountText;

    public int maxPebbles = 3;
    public TMP_Text maxPebblesText;
    public GameObject maxPebblesScreen;

    public TMP_Text noPebblesText;
    public GameObject noPebblesScreen;


    public bool canCollectPebble = true;
    public bool canNotCollectPebble = false;
    public bool canFire = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originaleStepOffset = characterController.stepOffset;
        pebbleCountText.text = pebbleCount.ToString();
    }

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
            //if (GetComponent<pPlayerComponent>().isInConversation) return;

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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (pebbleCount > 0)
            {

                canFire = true;
                FireSlingshot();
                pebbleCount--;
                pebbleCountText.text = pebbleCount.ToString();
                Debug.Log("Bla");
            }
            if (pebbleCount <= 0)
            {
                pebbleCount = 0;
                canFire = false;
                pebbleCountText.text = pebbleCount.ToString();
                noPebblesScreen.SetActive(true);
                StartCoroutine(ToggleNoPebbleText());
            }
         /* if (!pebbleInstantiated) { return; }

            pebblePrefab.transform.position = slingshotPivot.transform.position;*/
        }

        // Firing the slingshot
        /*if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            FireSlingshot();
        }

        if (canNotCollectPebble)
        {
            if (maxPebblesScreenTime < 3)
            {
                maxPebblesScreenTime = 0;
                maxPebblesScreen.SetActive(false);
            }
            maxPebblesScreenTime += Time.deltaTime;
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GroundPebble"))
        {
            if (pebbleCount < maxPebbles)
            {
                Destroy(other.gameObject);
                pebbleCount ++;
                pebbleCountText.text = pebbleCount.ToString();
            }
            else
            {
                canCollectPebble = false;
                canNotCollectPebble = true;
                maxPebblesScreen.SetActive(true);
                StartCoroutine(ToggleMaxPebbleText());
            }
        }
    }

    void PreppingSlingshot()
    {/*
        Instantiate(pebblePrefab, slingshotPivot.position, slingshotPivot.rotation);
        pebblePrefab.transform.parent = this.transform;
        pebbleInstantiated = true;*/
    }
    void FireSlingshot()
    {
        GameObject newPebble = Instantiate(pebblePrefab, slingshotPivot.position, slingshotPivot.rotation);
        pebblePrefab.GetComponent<Rigidbody>().AddForce(transform.forward * pebbleSpeed);
        newPebble.GetComponent<TestingPebbleShootingMechanic>().aimPos = slingshotPivot.position;
        newPebble.tag = "Pebble";
    }

    IEnumerator ToggleMaxPebbleText()
    {
        yield return new WaitForSeconds(1);
        maxPebblesScreen.SetActive(false);
        Debug.Log("Hello, please disappear");
    }

    IEnumerator ToggleNoPebbleText()
    {
        yield return new WaitForSeconds(1.5f);
        noPebblesScreen.SetActive(false);
    }
}
