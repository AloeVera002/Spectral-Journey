using UnityEngine;

public class AttackManager : MonoBehaviour
{
    int zombieHelth = 100;
    int bebbleDamage = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Pebble")){
            Debug.Log("Zombie is hit");
            zombieHelth -= bebbleDamage;
            if (zombieHelth <= 0){
                Destroy(gameObject);
                Debug.Log("Zobie wittewally ded");
            }

        }
    }
}
