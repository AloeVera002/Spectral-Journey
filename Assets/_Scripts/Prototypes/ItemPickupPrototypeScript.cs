using TMPro;
using Unity.Cinemachine;
using UnityEngine;

public class ItemPickupPrototypeScript : MonoBehaviour
{
    private bool inPickupZone;
    public GameObject pickupText;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
            other.gameObject.GetComponent<CoolAssPickupPlayerPrototypeScript>().canPickup = true;
            other.gameObject.GetComponent<CoolAssPickupPlayerPrototypeScript>().targetItem = this.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pickupText.SetActive(false);
    }
    private void Pickup()
    {
        Debug.Log("Player is in PickupZone");
        pickupText.SetActive(true);
    }
    private void OnDestroy()
    {
        pickupText.SetActive(false);
    }
}
