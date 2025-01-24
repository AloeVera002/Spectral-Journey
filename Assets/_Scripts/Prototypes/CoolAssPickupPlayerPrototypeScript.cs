using UnityEngine;

public class CoolAssPickupPlayerPrototypeScript : MonoBehaviour
{
    public bool canPickup = false;
    public int numberOfFlowers;
    public GameObject targetItem;

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
    }
}
