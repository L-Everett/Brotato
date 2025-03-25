//拳头
public class Weapon2 : WeaponShot
{
    private new void Awake()
    {
        base.Awake();
        angleOffset = 45f;
        moveSpeed = 25f;
    }
}
