using System.Collections;
using UnityEngine;

public class WeaponLong : WeaponBase
{
    public GameObject bullet;

    public new void Awake()
    {
        base.Awake();
        data.range = 10;
        data.damage = 8;
        data.cooling = 2f;
    }

    public override void DoAttack()
    {
        DoAnim();

        Transform arrow = Instantiate(bullet, transform.position, Quaternion.identity).transform;
        arrow.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + angleOffset);
        arrow.SetParent(GameObject.Find("Items").transform);

        arrow.GetComponent<Bullet>().SetDamage(data.damage);
        arrow.GetComponent<Bullet>().SetSpeed(moveSpeed);
        arrow.GetComponent<Bullet>().SetDirection(enemy.transform.position - transform.position);
        arrow.GetComponent<Bullet>().SetGo(true);
        arrow.GetComponent<Collider2D>().enabled = true;
    }

    private void DoAnim()
    {
        StartCoroutine(Anim());
    }

    IEnumerator Anim()
    {
        float speed = 8f;
        Vector3 dir = transform.position - enemy.transform.position;
        Vector3 originPisition = new Vector3(0, 0, 0);

        while (Vector3.Distance(transform.localPosition, originPisition) < 0.5f)
        {
            Vector2 move = speed * Time.deltaTime * dir.normalized;

            transform.localPosition += new Vector3(move.x, move.y, 0);
            yield return null;
        }

        dir = originPisition - transform.localPosition;

        while (Vector3.Distance(transform.localPosition, originPisition) > 0.1f)
        {
            Vector2 move = speed * Time.deltaTime * dir.normalized;
            transform.localPosition += new Vector3(move.x, move.y, 0);
            yield return null;
        }
        transform.localPosition = originPisition;
    }
}
