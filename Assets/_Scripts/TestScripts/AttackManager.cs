using UnityEngine;

public class AttackManager : MonoBehaviour
{
    int zombieHelth = 100;
    int pebbleDamage = 20;

    [SerializeField] bool isHeadShot = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other){

        if (!isHeadShot)
        {
            if (other.gameObject.CompareTag("Pebble"))
            {
                Debug.Log("Zombie is hit");
                zombieHelth -= pebbleDamage;
                if (zombieHelth <= 0)
                {
                    Destroy(gameObject);
                    Debug.Log("Zobie wittewally ded");
                }
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Pebble"))
            {
                Debug.Log("Zombie got hit with a fucking headshot swag gangster!:)");
                zombieHelth -= pebbleDamage * 2;
                if (zombieHelth <= 0)
                {
                    Destroy(gameObject);
                    Debug.Log("Zobie wittewally ded");
                }
            }

        }
    }
    
       
}
