//¹­
using UnityEngine;

public class Weapon4 : WeaponLong
{
    private new void Awake()
    {
        base.Awake();
        bullet = Resources.Load<GameObject>("prefabs/Arrow");
        angleOffset = 50f;
        moveSpeed = 15f;
    }
}
