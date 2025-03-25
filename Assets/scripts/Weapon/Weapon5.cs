//Ò½ÁÆÇ¹
using UnityEngine;

public class Weapon5 : WeaponLong
{
    private new void Awake()
    {
        base.Awake();
        bullet = Resources.Load<GameObject>("prefabs/MedicalGunBullet");
        angleOffset = 10f;
        moveSpeed = 15f;
    }
}
