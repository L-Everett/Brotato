using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float hp;       //Ѫ��
    public float damage;   //������
    public float speed;    //�ƶ��ٶ�
    public float attackTime; //�������
    public float attackTimer;//������ʱ��
    public bool isContact;   //�Ƿ�Ӵ������
    public bool isCooling;   //������ȴ
    public int proExp;      //�ṩ�ľ���ֵ

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
