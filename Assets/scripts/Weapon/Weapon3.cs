// ÷«π
using UnityEngine;

public class Weapon3 : WeaponLong
{
    private new void Awake()
    {
        base.Awake();
        bullet = Resources.Load<GameObject>("prefabs/PistolBullet");
        angleOffset = 10f;
        moveSpeed = 15f;
    }
}
