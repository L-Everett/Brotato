using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySelectPanel : MonoBehaviour
{
    public static DifficultySelectPanel instance;
    public CanvasGroup canvasGroup;
    public Transform difficultyContent;

    public List<DifficulityData> difficulitDatas = new List<DifficulityData> ();
    public TextAsset difficultyText;

    public GameObject difficultyPrefab;
    public Transform difficultyList;

    public Image avatar;
    public TextMeshProUGUI difficultyName;
    public TextMeshProUGUI difficultyDescribe;

    private void Awake()
    {
        instance = this;
        difficultyContent = GameObject.Find("DifficultyContent").transform;
        canvasGroup = GetComponent<CanvasGroup>();
        difficultyText = Resources.Load<TextAsset>("Data/difficulty");
        difficulitDatas = JsonConvert.DeserializeObject<List<DifficulityData>> (difficultyText.text);

        difficultyPrefab = Resources.Load<GameObject>("prefabs/Difficulty");
        difficultyList = GameObject.Find("DifficultyList").transform;

        avatar = GameObject.Find("Avatar_Difficulty").GetComponent<Image>();
        difficultyName = GameObject.Find("DifficultyName").GetComponent<TextMeshProUGUI>();
        difficultyDescribe = GameObject.Find("DifficultyDescribe").GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (DifficulityData data in difficulitDatas)
        {
            DifficulityUI difficulityUI =  Instantiate(difficultyPrefab, difficultyList).GetComponent<DifficulityUI>();
            difficulityUI.SetData(data);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
