using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UI;

public class DifficulityUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DifficulityData difficultyData;
    public Image avatar;
    public Image bg;
    public Button button;

    private void Awake()
    {
        avatar = transform.GetChild(0).GetComponent<Image>();
        bg = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        RenewUI();
    }

    private void RenewUI()
    {
        DifficultySelectPanel.instance.avatar.sprite = avatar.sprite;
        DifficultySelectPanel.instance.difficultyName.text = difficultyData.name;
        DifficultySelectPanel.instance.difficultyDescribe.text = GetDescribe();
    }

    private string GetDescribe()
    {
        string result = "";
        foreach (DifficulityData data in DifficultySelectPanel.instance.difficulitDatas)
        {
            if (data.id <= difficultyData.id)
            {
                result += data.describe + "\n";
            }
        }

        return result;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = new Color(37 / 255f, 37 / 255f, 37 / 255f);
    }

    internal void SetData(DifficulityData data)
    {
        difficultyData = data;
        avatar.sprite = Resources.Load<SpriteAtlas>("Image/UI/Î£ÏÕµÈ¼¶").GetSprite(data.name);

        button.onClick.AddListener(() =>
        {
            GameManager.Instance.difficulityData = data;
            SceneManager.LoadScene(2);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
