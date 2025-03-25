using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float speed = 8f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.money++;
            GamePanel.instance.RenewMoney();
            Destroy(gameObject);
        }
    }

    public void GoPlayer(Vector3 position)
    {
        StartCoroutine(Run(position));
    }

    IEnumerator Run(Vector3 position)
    {
        while(Vector3.Distance(position, transform.position) > 0.1f) 
        {
            Vector3 direction = position - transform.position;
            Vector3 move = speed * Time.deltaTime * direction;
            transform.position += move;
            yield return null;
        }
    }
}
