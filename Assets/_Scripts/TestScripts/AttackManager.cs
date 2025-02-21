using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public int hp;
    public int damage;
    public string tag;

    int hitPoints = 100;
    int incomingDamage = 20;

    [SerializeField] bool isHeadShot = false;
    [SerializeField] AudioClip hitAudio;

    private QuestManager qMan;

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
        if (other.gameObject.CompareTag(tag))
        {
            //    Instantiate(GetComponent<pPlayerComponent>().pebbleGround, other.gameObject.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    void HitHandler()
    {
        CheckIfHeadShot();
        Debug.Log("Hit: " + this.gameObject.tag + "Damage dealt: " + incomingDamage);
        GameObject.Find("PlayerV2").GetComponent<pPlayerComponent>().soundAudioSource.PlayOneShot(hitAudio);
        hitPoints -= incomingDamage;
        if (hitPoints <= 0)
        {
            OnDeath?.Invoke(this.gameObject.tag);
            if (GameObject.Find("PlayerV2").GetComponent<QuestManager>().currentQuest.QuestType == QuestTypeEnum.Kill)
            {
                Debug.Log(this.gameObject.tag + " ueq" + " current quest is of type: " + GameObject.Find("PlayerV2").GetComponent<QuestManager>().currentQuest.QuestType + " tried to invoke OnDeathEvent");
            //    OnDeath?.Invoke(this.gameObject.tag);
            }
            else
            {
                Debug.Log(this.gameObject.tag + " wue" + " current quest is of type: " + GameObject.Find("PlayerV2").GetComponent<QuestManager>().currentQuest.QuestType);
            }
            Debug.Log(this.gameObject.tag + " wittewally ded");
        }
    }

    void CheckIfHeadShot()
    {
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
        {
            if (damage > 0)
            {
                incomingDamage = damage * 2;
            }
            else
            {
                incomingDamage = 20 * 2;
            }
        }
    }

    void DeathEventHandler(string tag)
    {
        switch (tag)
        {
            case "Zombie":
                {
                    if (this.gameObject.name == "Enemy")
                    {

                        if (qMan != null)
                        {
                            qMan.CallQuestObjectiveEvent();
                            GameObject newGhost = GameObject.Find("HiddenGhost");
                            newGhost.transform.position = this.transform.position;
                            newGhost.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("qMan for DeathEvent null");
                        }
                    }
                    else
                    {
                        Debug.Log("this object is not Enemy");
                    }
                }
                break;
            case "Player":
                Debug.Log("haha you died");
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}

