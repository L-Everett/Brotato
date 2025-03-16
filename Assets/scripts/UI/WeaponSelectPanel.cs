using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectPanel : MonoBehaviour
{
    public static WeaponSelectPanel instance;
    public CanvasGroup canvasGroup;
    public Transform weaponCotent;

    public List<WeaponData> weaponDatas;
    public TextAsset weaponTextAsset;
    public GameObject weaponPrefab;
    public Transform weaponList;

    public Image avatarWeapon;
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponIsLong;
    public TextMeshProUGUI weaponDescription;

    public GameObject weaponDetails;

    private void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        weaponCotent = GameObject.Find("WeaponContent").transform;
        weaponList = GameObject.Find("WeaponList").transform;
        weaponTextAsset = Resources.Load<TextAsset>("Data/weapon");
        weaponDatas = JsonConvert.DeserializeObject<List<WeaponData>>(weaponTextAsset.text);
        weaponPrefab = Resources.Load<GameObject>("prefabs/Weapon");

        avatarWeapon = GameObject.Find("Avatar_Weapon").GetComponent<Image>();
        weaponName = GameObject.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        weaponIsLong = GameObject.Find("WeaponRange").GetComponent<TextMeshProUGUI>();
        weaponDescription = GameObject.Find("WeaponDescribe").GetComponent<TextMeshProUGUI>();

        weaponDetails = GameObject.Find("WeaponDetails");
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (WeaponData weapon in weaponDatas)
        {
            WeaponUI weaponUI = Instantiate(weaponPrefab, weaponList).GetComponent<WeaponUI>();
            weaponUI.SetData(weapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
