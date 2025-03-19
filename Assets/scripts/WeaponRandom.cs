using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponRandom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image bg;
    public Button button;
    public List<WeaponUI> weapons = new List<WeaponUI>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        WeaponSelectPanel.instance.avatarWeapon.sprite = Resources.Load<Sprite>("Image/UI/ÎÊºÅ");
        WeaponSelectPanel.instance.weaponName.text = "???";
        WeaponSelectPanel.instance.weaponDescription.text = "Ëæ»úÑ¡Ôñ";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = new Color(37 / 255f, 37 / 255f, 37 / 255f);
    }

    private void Awake()
    {
        bg = GetComponent<Image>();
        button = GetComponent<Button>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            foreach (WeaponUI weapon in WeaponSelectPanel.instance.weaponList.GetComponentsInChildren<WeaponUI>())
            {
                weapons.Add(weapon);
            }
            WeaponUI w = GameManager.Instance.RandomSelect<WeaponUI>(weapons) as WeaponUI;
            w.RenewUI();
            w.OnSelectWeapon();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
