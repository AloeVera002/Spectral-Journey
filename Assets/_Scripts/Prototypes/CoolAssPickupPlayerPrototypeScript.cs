using UnityEngine;
using TMPro;

public class CoolAssPickupPlayerPrototypeScript : MonoBehaviour
{
    public bool canPickup = false;
    public int numberOfFlowers = 0;
    public GameObject targetItem = null;
    public TMP_Text questText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickupFlower();
        } 

    } 
    void PickupFlower()
    { if (!canPickup) return;
    
        Destroy(targetItem);
        numberOfFlowers += 1;
        canPickup = false;
        targetItem = null;
    }
}
