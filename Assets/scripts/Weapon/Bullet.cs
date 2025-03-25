using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 1f;
    private float speed = 1f;
    private bool go = false;
    private Vector3 direction;

    private void Update()
    {
        if (go)
        {
            Go();
        }
    }


    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetGo(bool go)
    {
        this.go = go;
    }

    public void SetDirection(Vector3 direction)
    {
        direction = direction.normalized;
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().Injured(damage);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    private void Go()
    {
        Vector3 move = speed * Time.deltaTime * direction;
        transform.position += move;
    }
}
