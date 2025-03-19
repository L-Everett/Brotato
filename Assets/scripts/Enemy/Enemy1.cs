public class Enemy1 : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        damage = 1;
        proExp = 1;
        hp = 8f;
        isCooling = false;
        isContact = false;
        attackTime = 2;
        attackTimer = 0;
    }

}
