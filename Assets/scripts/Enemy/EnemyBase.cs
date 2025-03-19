using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float hp;       //血量
    public float damage;   //攻击力
    public float speed;    //移动速度
    public float attackTime; //攻击间隔
    public float attackTimer;//攻击定时器
    public bool isContact;   //是否接触到玩家
    public bool isCooling;   //攻击冷却
    public int proExp;      //提供的经验值

    public GameObject moneyPrefab;

    private void Awake()
    {
        moneyPrefab = Resources.Load<GameObject>("prefabs/Money");
    }

    public void Update()
    {
        Move();

        if (isContact && !isCooling)
        {
            Attack();
        }

        if (isCooling)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isContact = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isContact = false;
        }
    }

    public void Move()
    {
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
        TurnAround();
    }

    public void TurnAround()
    {
        if(Player.Instance.transform.position.x - transform.position.x > 0.1) 
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(Player.Instance.transform.position.x - transform.position.x < 0.1)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Attack()
    {
        if (isCooling)
        {
            return;
        }
        Player.Instance.Injured(damage);
        isCooling = true;
        attackTimer = attackTime;
    }

    public void Injured(float attack)
    {
        if (hp - attack <= 0)
        {
            hp = 0;
            Death();
        }
        else
        {
            hp -= attack;
        }
    }

    public void Death()
    {
        GameObject money =  Instantiate(moneyPrefab, GameObject.Find("Items").transform);
        money.transform.position = transform.position;
        Player.Instance.exp += proExp;
        GamePanel.instance.RenewExp();

        Destroy(gameObject);
    }

}
