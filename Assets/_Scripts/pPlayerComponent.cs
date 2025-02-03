using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pPlayerComponent : MonoBehaviour
{
    [SerializeField] public bool isInteracting = false;
    [SerializeField] public bool isInConversation = false;

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
            SwitchCamera();
            ectoplasm++;
        }

        ectroplasmText.text = ectoplasm.ToString();
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
        SceneManager.LoadScene(1);
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