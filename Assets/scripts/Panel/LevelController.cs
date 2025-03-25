using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public float waveTimer;
    public float initTime;   //初始场景时间
    public float frushTimer; //刷怪计时器
    public float frushInterval; //刷怪间隔
    public bool generate;
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
        waveTimer = 0;
        initTime = 5f;
        RenewTime();
        frushTimer = 2f;
        frushInterval = 3f;
        generate = true;
    }

    public void RenewTime()
    {
        if (waveTimer <= 60f)
        {
            waveTimer = initTime + 10 * GameManager.Instance.currentWave;
        }
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

        if(frushTimer > 0 && generate)
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
        successPanel.GetComponent<CanvasGroup>().alpha = 1;
        generate = false;
        Player.Instance.playerMove = false;
        foreach(Transform item in GameObject.Find("Items").transform)
        {
            if (item.GetComponent<Item>())
            {
                item.GetComponent<Item>().GoPlayer(Player.Instance.transform.position);
            }
        }
        foreach (Transform item in GameObject.Find("Enemies").transform)
        {
            if (item.GetComponent<EnemyBase>())
            {
                item.GetComponent<EnemyBase>().Kill();
            }
        }
        StartCoroutine(NextLevel());
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

    IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(3);
        //Time.timeScale = 1;
        RenewTime();
        GamePanel.instance.RenewWaveCount();
        frushInterval /= 1.5f;
        successPanel.GetComponent<CanvasGroup>().alpha = 0;
        generate = true;
        Player.Instance.playerMove = true;
    }
}
