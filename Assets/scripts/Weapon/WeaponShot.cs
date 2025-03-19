using UnityEngine;

public class WeaponShot : WeaponBase
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Injured(data.damage);
            gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }
}
