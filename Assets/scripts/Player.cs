using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Transform playervisual;
    public Animator animatorLeg1;
    public Animator animatorLeg2;

    public int money;
    public int exp;
    public float speed;
    public float hp;
    public float maxHp;
    public bool isDead;

    public int wId;
    public int wCount;

    public bool playerMove;

    private void Awake()
    {
        Instance = this;
        playervisual = GameObject.Find("PlayerVisual").transform;
        animatorLeg1 = GameObject.Find("Leg1").GetComponent<Animator>();
        animatorLeg2 = GameObject.Find("Leg2").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
/*        if (GameManager.Instance != null)
        {
            wCount = GameManager.Instance.weaponCount;
            wId = GameManager.Instance.weaponData.id;
        }*/

        for (int i = 0; i < wCount; i++)
        {
            ActivateWeapon(i + 1, wId);
        }
        playerMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMove)
        {
            PlayerMove();
        }
    }

    public void ActivateWeapon(int positionId, int weaponId)
    {
        string name = "w" + positionId;
        GameObject.Find(name).transform.GetChild(weaponId).gameObject.SetActive(true);
    }

    public void PlayerMove()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(moveHorizontal, moveVertical);

        transform.Translate(move * speed * Time.deltaTime);

        TurnAround(moveHorizontal);

        if(move.magnitude != 0) 
        {
            animatorLeg1.SetBool("isRun", true);
            animatorLeg2.SetBool("isRun", true);
        }
        else
        {
            animatorLeg1.SetBool("isRun", false);
            animatorLeg2.SetBool("isRun", false);
        }

        transform.rotation = Quaternion.identity;
    }

    public void TurnAround(float h)
    {
        if(h == 1 || h == -1)
        {
            playervisual.localScale = new Vector3(h, 1, 1);
        }
    }

    public void Injured(float attack)
    {
        if(hp - attack <= 0)
        {
            hp = 0;
            GamePanel.instance.RenewHp();
            Death();
        }
        else
        {
            hp -= attack;
            GamePanel.instance.RenewHp();
        }
    }

    public void Death()
    {
        isDead = true;
        playerMove = false;
        animatorLeg1.speed = 0;
        animatorLeg2.speed = 0;

        LevelController.instance.BadGame();
    }
}
