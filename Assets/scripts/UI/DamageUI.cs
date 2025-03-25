using UnityEngine;

public class DamageUI : MonoBehaviour
{
    public float showTime = 1f;
    // Update is called once per frame
    void Update()
    {
        showTime -= Time.deltaTime;
        if (showTime < 0f)
        {
            Destroy(gameObject);
        }
    }
}
