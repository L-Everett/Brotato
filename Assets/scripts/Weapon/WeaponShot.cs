using UnityEngine;
using System.Collections;

public class WeaponShot : WeaponBase
{
    public new void Awake()
    {
        base.Awake();
        data.range = 5;
        data.damage = 3;
        data.cooling = 0.8f;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Injured(data.damage);
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }

    public override void DoAttack()
    {
        StartCoroutine(GoPosition());
    }

    IEnumerator GoPosition()
    {
        var enemyPos = enemy.transform.position;
        gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        while (Vector2.Distance(transform.position, enemyPos) > 0.1f)
        {
            Vector3 direction = (enemyPos - transform.position).normalized;

            Vector3 moveAmount = direction * moveSpeed * Time.deltaTime;

            transform.position += moveAmount;

            yield return null;
        }

        //gameObject.GetComponent<EdgeCollider2D>().enabled = false;

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
