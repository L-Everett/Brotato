using System;
using System.Collections;
using System.Linq;
using System.Net.Mail;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public WeaponData data;
    public bool isAttack = false;
    public bool isCooling = false;
    public bool isAiming = true;
    public float attackTimer = 0;
    public float moveSpeed;
    public Transform enemy;
    public float iniAngleZ;

    private void Awake()
    {
        iniAngleZ = transform.eulerAngles.z;
    }

    public void Update()
    {
        Aiming();

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
            isAttack = true;
            Collider2D nearestEnemy = enemiesInRange.
                OrderBy(enemy=>Vector2.Distance(transform.position,enemy.transform.position)).
                First();
            enemy = nearestEnemy.transform;

            Vector2 enemyPos = enemy.position;
            Vector2 direction = enemyPos - (Vector2)transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }
        else
        {
            isAttack = false;
            enemy = null;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, iniAngleZ);
        }
    }

    public void Fire()
    {
        if (isCooling)
        {
            return;
        }

        gameObject.GetComponent<EdgeCollider2D>().enabled = true;

        isAiming = false;
        StartCoroutine(GoPosition());

        isCooling = true;
    }

    IEnumerator GoPosition()
    {
        var enemyPos = enemy.transform.position;
        while(Vector2.Distance(transform.position,enemyPos) > 0.1f)
        {
            Vector3 direction = (enemyPos - transform.position).normalized;

            Vector3 moveAmount = direction * moveSpeed * Time.deltaTime;

            transform.position += moveAmount;

            yield return null;
        }

        gameObject.GetComponent<EdgeCollider2D>().enabled = false;

        StartCoroutine(ReturnPosition());
    }

    IEnumerator ReturnPosition()
    {
        while ((Vector3.zero - transform.localPosition).magnitude > 0.1f)
        {
            Vector3 direction = (Vector3.zero - transform.localPosition).normalized;
            transform.localPosition += direction * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
