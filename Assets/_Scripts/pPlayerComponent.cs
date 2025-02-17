using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pPlayerComponent : MonoBehaviour
{
    [SerializeField] List<GameObject> listTest = new List<GameObject>();

    [SerializeField] public bool isInteracting = false;
    [SerializeField] public bool isInConversation = false;
    [SerializeField] private Transform checkPoint;

    Animator animator;

    public Camera pCamera;
    [SerializeField] Vector3 dialogueCamera;
    [SerializeField] Vector3 normalCamera;
    [SerializeField] Vector3 dialogueRotation;

    [SerializeField] public AudioSource soundAudioSource;

    public bool canCollectPebble = true;
    public bool canNotCollectPebble = false;

    public int pebbleCount = 3;
    public TMP_Text pebbleCountText;

    public int maxPebbles = 3;
    public TMP_Text maxPebblesText;
    public GameObject maxPebblesScreen;

    public TMP_Text noPebblesText;
    public GameObject noPebblesScreen;

    [SerializeField] public TMP_Text ectroplasmText;

    public Transform slingshotPivot;
    public GameObject slingshot;
    public GameObject pebblePrefab, pebbleGround;

    public int ectoplasm;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateText(pebbleCountText, pebbleCount.ToString());
        UpdateText(ectroplasmText, ectoplasm.ToString());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ectoplasm++;
            listTest.Remove(listTest[0]);
        }
    }

    public void ToggleDialogueCamera()
    {
        if (!isInConversation)
        {
            pCamera.GetComponent<CameraController>().offset = dialogueCamera;
            StartCoroutine(RotateCamera(dialogueRotation));
        }
        else
        {
            pCamera.GetComponent<CameraController>().offset = normalCamera;
            StartCoroutine(RotateCamera(new Vector3(40, 0, 0)));
        }
        isInConversation = !isInConversation;
    }

    private IEnumerator RotateCamera(Vector3 end)
    {
        float timeCount = 0f;

        while (timeCount < 0.15f)
        {
            timeCount += Time.deltaTime * .5f;
            pCamera.transform.rotation = Quaternion.Lerp(pCamera.transform.rotation, Quaternion.Euler(end), timeCount);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") || other.gameObject.CompareTag("Zombie") || other.gameObject.CompareTag("FallOffWorld"))
        {
            WaterDeath();
        }
        if (other.gameObject.CompareTag("GroundPebble"))
        {
            if (pebbleCount < maxPebbles)
            {
                Destroy(other.gameObject);
                soundAudioSource.PlayOneShot(GetComponent<QuestManager>().pickupSound);
                IncreasePebbleCount(1);
                UpdatePebbleText();
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

    void WaterDeath()
    {
        Debug.Log("Teleportation commenced");
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = checkPoint.position;
        gameObject.GetComponentInChildren<AimAssister>().ResetAimTarget();
        this.gameObject.SetActive(true);
    }

    IEnumerator ToggleMaxPebbleText()
    {
        yield return new WaitForSeconds(1);
        maxPebblesScreen.SetActive(false);
    }

    IEnumerator ToggleNoPebbleText()
    {
        yield return new WaitForSeconds(1.5f);
        noPebblesScreen.SetActive(false);
    }

    public void ActivateNoPebbleText()
    {
        StartCoroutine(ToggleNoPebbleText());
    }

    public void SetPebbleCount(int newCount)
    {
        pebbleCount = newCount;
    }

    public void IncreasePebbleCount(int increaseAmount)
    {
        pebbleCount += increaseAmount;
    }

    public void DecreasePebbleCount(int decreaseAmount)
    {
        pebbleCount -= decreaseAmount;
    }

    public void UpdatePebbleText()
    {
        UpdateText(pebbleCountText, pebbleCount.ToString());
    }

    public void UpdateText(TMP_Text texter, string newText)
    {
        texter.text = newText;
    }
}