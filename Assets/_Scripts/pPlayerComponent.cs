using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pPlayerComponent : MonoBehaviour
{
    [SerializeField] public bool isInteracting = false;
    [SerializeField] public bool isInConversation = false;
    [SerializeField] private Transform checkPoint;

    Animator animator;

    public bool canCollectPebble = true;
    public bool canNotCollectPebble = false;

    public int pebbleCount = 3;
    public TMP_Text pebbleCountText;

    public int maxPebbles = 3;
    public TMP_Text maxPebblesText;
    public GameObject maxPebblesScreen;

    public TMP_Text noPebblesText;
    public GameObject noPebblesScreen;

    [SerializeField] TMP_Text ectroplasmText;

    public Transform slingshotPivot;
    public GameObject slingshot;
    public GameObject pebblePrefab;

    public int ectoplasm;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateText(pebbleCountText, pebbleCount.ToString());
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            ectoplasm++;
        }

        //ectroplasmText.text = ectoplasm.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            WaterDeath();
        }
        if (other.gameObject.CompareTag("GroundPebble"))
        {
            if (pebbleCount < maxPebbles)
            {
                Destroy(other.gameObject);
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
        if (other.gameObject.CompareTag("Zombie"))
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("ZombieTestScene");
        }
    }

    void WaterDeath()
    {
        Debug.Log("before " + this.gameObject.transform.position + "before checkpoint " + checkPoint.position);
        
        this.gameObject.transform.position = checkPoint.position;
        Debug.Log("after " + this.gameObject.transform.position + "after checkpoint " + checkPoint.position);
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

    void UpdateText(TMP_Text texter, string newText)
    {
        texter.text = newText;
    }
}