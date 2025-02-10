using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public int hp;
    public int damage;
    public string tag;

    int hitPoints = 100;
    int incomingDamage = 20;

    [SerializeField] bool isHeadShot = false;

    void Start()
    {
        if (hp > 0) { hitPoints = hp; }
        if (damage > 0) { incomingDamage = damage; }
    }

    void OnCollisionEnter(Collision other){

        if (!isHeadShot)
        {
            if (damage > 0)
            {
                incomingDamage = damage;
            }
            else
            {
                incomingDamage = 20;        
            }
        }
        else
        {/*
            if (other.gameObject.CompareTag("Pebble"))
            {
                Debug.Log("Zombie got hit with a fucking headshot swag gangster!:)");
                zombieHelth -= pebbleDamage * 2;
                if (zombieHelth <= 0)
                {
                    Destroy(gameObject);
                    Debug.Log("Zobie wittewally ded");
                }
            }*/
            if (damage > 0)
            {
                incomingDamage = damage * 2;
            }
            else
            {
                incomingDamage = 20 * 2;
            }
        }
        if (other.gameObject.CompareTag(tag))
        {
            Debug.Log("Zombie is hit");
            hitPoints -= incomingDamage;
            if (hitPoints <= 0)
            {
                Destroy(gameObject);
                if (GameObject.Find("PlayerV2").GetComponent<QuestManager>().currentQuest.isKilling)
                {
                    GameObject.Find("PlayerV2").GetComponent<QuestManager>().CompleteQuest();
                }
                Debug.Log("Zobie wittewally ded");
            }
        }
    }
    
       
}
