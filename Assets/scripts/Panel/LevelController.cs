using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public float waveTimer;
    public float frushTimer; //Ë¢¹Ö¼ÆÊ±Æ÷
    public float frushInterval; //Ë¢¹Ö¼ä¸ô
    public Transform map;
    public GameObject failPanel;
    public GameObject successPanel;

    public GameObject enemy1_prefab;

    private void Awake()
    {
        instance = this;
        map = GameObject.Find("MapImg").transform;
        failPanel = GameObject.Find("FailPanel");
        successPanel = GameObject.Find("SuccessPanel");
        enemy1_prefab = Resources.Load<GameObject>("prefabs/Enemy1");
    }
    // Start is called before the first frame update
    void Start()
    {
        waveTimer = 55 + 5 * GameManager.Instance.currentWave;
        frushTimer = 2;
        frushInterval = 2;
    }

    IEnumerator GenerateEnemies()
    {
        yield return new WaitForSeconds(0.5f);
        
        Vector3 spawnPoint = GetRandomPosition(map.GetComponent<SpriteRenderer>().bounds);
        GameObject enemy = Instantiate(enemy1_prefab, GameObject.Find("Enemies").transform);
        enemy.transform.position = spawnPoint;
    }

    private Vector3 GetRandomPosition(Bounds bounds)
    {
        float safeDis = 3.5f;
        float randomX = UnityEngine.Random.Range(bounds.min.x + safeDis, bounds.max.x - safeDis);
        float randomY = UnityEngine.Random.Range(bounds.min.y + safeDis, bounds.max.y - safeDis);
        float randomZ = 0;
        return new Vector3(randomX, randomY, randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        if(waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if(waveTimer <= 0) 
            { 
                waveTimer = 0;
                GoodGame();
            }    
        }

        if(frushTimer > 0)
        {
            frushTimer -= Time.deltaTime;
            if (frushTimer <= 0)
            {
                StartCoroutine(GenerateEnemies());
                frushTimer = frushInterval;
            }
        }

        GamePanel.instance.RenewCountDown(waveTimer);
    }


    public void GoodGame()
    {
        Time.timeScale = 0;
        successPanel.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(Gomenu());
    }

    public void BadGame()
    {
        Time.timeScale = 0;
        failPanel.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(Gomenu());
    }

    IEnumerator Gomenu()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
