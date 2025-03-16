using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    WeaponData weaponData;
    private Image bg;
    private Image avatar;
    private Button button;

    private void Awake()
    {
        bg = GetComponent<Image>();
        avatar = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void SetData(WeaponData data)
    {
        weaponData = data;
        avatar.sprite = Resources.Load<Sprite>(data.avatar);
        button.onClick.AddListener(() =>
        {
            RenewUI();
            OnSelectWeapon();
        });
    }

    public void OnSelectWeapon()
    {
        GameManager.Instance.WeaponDatas.Add(weaponData);

        GameObject weaponContentClone = Instantiate(WeaponSelectPanel.instance.weaponDetails, DifficultySelectPanel.instance.difficultyContent);
        weaponContentClone.transform.SetSiblingIndex(0);

        GameObject roleContentClone = Instantiate(RoleSelectPanel.instance.roleDetail, DifficultySelectPanel.instance.difficultyContent);
        roleContentClone.transform.SetSiblingIndex(0);

        WeaponSelectPanel.instance.canvasGroup.alpha = 0;
        WeaponSelectPanel.instance.canvasGroup.interactable = false;
        WeaponSelectPanel.instance.canvasGroup.blocksRaycasts = false;

        DifficultySelectPanel.instance.canvasGroup.alpha = 1;
        DifficultySelectPanel.instance.canvasGroup.interactable = true;
        DifficultySelectPanel.instance.canvasGroup.blocksRaycasts = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = new Color(34 / 255f, 34 / 255f, 34 / 255f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        RenewUI();
    }

    public void RenewUI()
    {
        WeaponSelectPanel.instance.avatarWeapon.sprite = Resources.Load<Sprite>(weaponData.avatar);
        WeaponSelectPanel.instance.weaponName.text = weaponData.name;
        WeaponSelectPanel.instance.weaponDescription.text = weaponData.describe;
        WeaponSelectPanel.instance.weaponIsLong.text = GetRange(weaponData.isLong);
    }

    private string GetRange(int isLong)
    {
        if (isLong == 0)
        {
            return "½ü³Ì";
        }
        return "Ô¶³Ì";
    }
}
