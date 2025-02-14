using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] QuestManager qMan;

    public int hp;
    public int damage;
    public string tag;

    int hitPoints = 100;
    int incomingDamage = 20;

    [SerializeField] bool isHeadShot = false;

    public delegate void DeathEvent(string tag);
    public event DeathEvent OnDeath;

    public delegate void HitEvent();
    public event HitEvent OnHit;

    void Start()
    {
        if (hp > 0) { hitPoints = hp; }
        if (damage > 0) { incomingDamage = damage; }

        qMan = GameObject.Find("PlayerV2").GetComponent<QuestManager>();

        OnDeath += DeathEventHandler;
        OnHit += HitHandler;
    }

    void OnCollisionEnter(Collision other)
    {
        OnHit?.Invoke();
    }

    void HitHandler()
    {
        damage = CheckIfHeadShot();
        Debug.Log("Hit: " + tag);
        hitPoints -= incomingDamage;
        if (hitPoints <= 0)
        {
            Destroy(gameObject);
            if (GameObject.Find("PlayerV2").GetComponent<QuestManager>().currentQuest.QuestType == QuestTypeEnum.Kill)
            {
                    OnDeath?.Invoke(this.gameObject.tag);
            }
            Debug.Log(tag + " wittewally ded");
        }
    }

    int CheckIfHeadShot()
    {
        if (!isHeadShot)
        {
            if (damage > 0)
            {
                 return incomingDamage = damage;
            }
            else
            {
                 return incomingDamage = 20;
            }
        }
        else
        {
            if (damage > 0)
            {
                return incomingDamage = damage * 2;
            }
            else
            {
                return incomingDamage = 20 * 2;
            }
        }
    }
    
    void DeathEventHandler(string tag)
    {
        if (tag == "Zombie")
        {
            if (qMan != null)
            {
                qMan.CallQuestObjectiveEvent();
            }
        }
        else if (tag == "Player")
        {
            Debug.Log("haha you died");
        }
    }
}
