using UnityEngine;
using TMPro;

public class CoolAssPickupPlayerPrototypeScript : MonoBehaviour
{
    public bool canPickup = false;
    public int numberOfFlowers = 0;
    public GameObject targetItem = null;
    public TMP_Text scoreText;

    void Start()
    {
        // Optionally, initialize the score display
        scoreText.text = numberOfFlowers.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupFlower();
            scoreText.text = numberOfFlowers.ToString(); // Correctly update the score
        }
    }

    void PickupFlower()
    {
        if (!canPickup) return;

        Destroy(targetItem);
        numberOfFlowers += 1;
        canPickup = false;
        targetItem = null;
    }
}
