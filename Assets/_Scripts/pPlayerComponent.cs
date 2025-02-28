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
    [SerializeField] public bool canInteract = false;
    [SerializeField] public bool isInConversation = false;
    [SerializeField] private Transform checkPoint;
    [SerializeField] string playerName = "Charlie";

    Animator animator;

    public Camera pCamera;
    [SerializeField] Vector3 dialogueCamera;
    [SerializeField] Vector3 normalCamera;
    [SerializeField] Vector3 dialogueRotation;

    public bool tutorialQuestDone = false;

    [SerializeField] GameObject pebbleHUDField;
    [SerializeField] GameObject pebbleUIPrefab;

    [SerializeField] public AudioSource soundAudioSource;
    public float audioPitch = .3f; // easter egg horrible

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
        DontDestroyOnLoad(this);
        animator = GetComponent<Animator>();
        UpdateText(ectroplasmText, ectoplasm.ToString());
    }

    public void InitPebblesHUD()
    {
        InitializePebblesHUD(maxPebbles);
        IncreasePebbleHUD();
    }

    void Update()
    {/*
        if (Input.GetKeyUp(KeyCode.R))
        {
            ectoplasm++;
            listTest.Remove(listTest[0]);
        }*/

        if (Input.GetKeyDown(KeyCode.G))
        {
            pebbleCount = 999;
            tutorialQuestDone = true;
            GetComponent<pPlayerControlls>().movementSpeed = 25;
        }
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void ToggleDialogueCamera()
    {
        if (isInConversation)
        {
            pCamera.GetComponent<CameraController>().offset = dialogueCamera;
            pCamera.GetComponent<CameraController>().target = GetComponent<DialogueManager>().oppositeTalker.gameObject;
            StartCoroutine(RotateCamera(dialogueRotation));
        }
        else
        {
            pCamera.GetComponent<CameraController>().offset = normalCamera;
            pCamera.GetComponent<CameraController>().target = this.gameObject;
            StartCoroutine(RotateCamera(new Vector3(40, 0, 0)));
        }
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
            if (!tutorialQuestDone) return;

            if (pebbleCount < maxPebbles)
            {
                Destroy(other.gameObject);
                soundAudioSource.pitch = Random.Range(audioPitch -0.05f, audioPitch + 0.05f);
                soundAudioSource.PlayOneShot(GetComponent<QuestManager>().pickupSound);
                IncreasePebbleCount(1);
            }
            else
            {
                canCollectPebble = false;
                canNotCollectPebble = true;
                maxPebblesScreen.SetActive(true);
                StartCoroutine(ToggleMaxPebbleText());
            }
        }
        else
        {
            soundAudioSource.pitch = 1f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
   //     canInteract = false;
    }

    void WaterDeath()
    {
        Debug.Log("Teleportation commenced");
        this.gameObject.SetActive(false);
        this.gameObject.transform.position = checkPoint.position;
        this.gameObject.SetActive(true);

        if (ectoplasm > 0)
        {
            ectoplasm -= 10;
            if (ectoplasm < 0)
            {
                ectoplasm = 0;
            }
        }
        else
        {
            ectoplasm = 0;
        }
        UpdateText(ectroplasmText, ectoplasm.ToString());

        gameObject.GetComponentInChildren<AimAssister>().ResetAimTarget();
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

    void InitializePebblesHUD(int count)
    {
        foreach (Transform child in pebbleHUDField.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < count; i++)
        {
            GameObject pebbleUIElement = Instantiate(pebbleUIPrefab, pebbleHUDField.transform);

            RectTransform rectTransform = pebbleUIElement.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(i * 75, 0);
        }
    }

    void DecreasePebbleHUD()
    {
        Debug.Log("decreased pebble ui");
        for (int i = pebbleHUDField.transform.childCount - 1; i >= 0; i--)
        {
            GameObject pebbleElement = pebbleHUDField.transform.GetChild(i).gameObject;

            if (i >= pebbleCount)
            {
                pebbleElement.SetActive(false);
            }
        }
    }

    void IncreasePebbleHUD()
    {
        Debug.Log("increased pebble ui");
        for (int i = 0; i < pebbleHUDField.transform.childCount; i++)
        {
            GameObject pebbleElement = pebbleHUDField.transform.GetChild(i).gameObject;

            if (i < pebbleCount)
            {
                pebbleElement.SetActive(true);
            }
        }
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
        IncreasePebbleHUD();
    }

    public void DecreasePebbleCount(int decreaseAmount)
    {
        pebbleCount -= decreaseAmount;
        DecreasePebbleHUD();
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