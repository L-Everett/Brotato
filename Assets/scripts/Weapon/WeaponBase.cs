using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponData data;
    public bool isAttack = false;
    public bool isCooling = false;
    public bool isAiming = true;
    public float attackTimer = 0;
    public float moveSpeed = 10f;
    public Transform enemy;
    public float iniAngleZ;
    public bool flipX;
    public bool flipY;
    public float angleOffset;

    public void Awake()
    {
        iniAngleZ = transform.eulerAngles.z;
        angleOffset = 0;
        flipX = GetComponent<SpriteRenderer>().flipX;
        flipY = GetComponent<SpriteRenderer>().flipY;

        if (GameManager.Instance)
        {
            data = GameManager.Instance.weaponData;
        }
    }

    public void Update()
    {
        if (isAiming)
        {
            Aiming();
        }

        if (!isCooling && isAttack)
        {
            Fire();
            isAiming = true;
        }

        if (isCooling)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= data.cooling)
            {
                attackTimer = 0;
                isCooling = false;
            }
        }
    }

    public void Aiming()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(
            transform.position, data.range, LayerMask.GetMask("Enemy"));

        if (enemiesInRange.Length > 0)
        {
            StartCoroutine(SetIsAttack());
            //isAttack = true;
            Collider2D nearestEnemy = enemiesInRange.
                OrderBy(enemy=>Vector2.Distance(transform.position,enemy.transform.position)).
                First();
            enemy = nearestEnemy.transform;

            Vector2 enemyPos = enemy.position;
            Vector2 direction = enemyPos - (Vector2)transform.position;
            float aimAngleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<SpriteRenderer>().flipY = false;
            transform.eulerAngles = new Vector3(0, 0, aimAngleZ - angleOffset);
        }
        else
        {
            isAttack = false;
            enemy = null;
            GetComponent<SpriteRenderer>().flipX = flipX;
            GetComponent<SpriteRenderer>().flipY = flipY;
            transform.eulerAngles = new Vector3(0, 0, iniAngleZ);
        }
    }

    IEnumerator SetIsAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttack = true;
    }

    public void Fire()
    {
        if (isCooling)
        {
            return;
        }

        isAiming = false;

        DoAttack();

        isCooling = true;
    }

    

    public virtual void DoAttack() { }
}
