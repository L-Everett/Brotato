using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RoleData roleData;
    private Image bg;
    private Image avatar;
    private Button button;

    private void Awake()
    {
        bg = GetComponent<Image>();
        avatar = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void SetData(RoleData roleData)
    {
        this.roleData = roleData;
        if(roleData.unlock == 0)
        {
            avatar.sprite = Resources.Load<Sprite>("Image/UI/锁");
        }
        else
        {
            avatar.sprite = Resources.Load<Sprite>(roleData.avatar);

            button.onClick.AddListener(() =>
            {
                OnSelectRole();
            });
        }
    }

    public void OnSelectRole()
    {
        //保存角色信息
        GameManager.Instance.RoleData = roleData;

        //关闭角色选择面板
        RoleSelectPanel.instance.canvasGroup.alpha = 0;
        RoleSelectPanel.instance.canvasGroup.interactable = false;
        RoleSelectPanel.instance.canvasGroup.blocksRaycasts = false;

        //复制角色UI
        GameObject roleDetail = Instantiate(RoleSelectPanel.instance.roleDetail, WeaponSelectPanel.instance.weaponCotent);
        roleDetail.transform.SetSiblingIndex(0);

        //打开武器选择面板
        WeaponSelectPanel.instance.canvasGroup.alpha = 1;
        WeaponSelectPanel.instance.canvasGroup.interactable = true;
        WeaponSelectPanel.instance.canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        RenewUI(roleData);
    }

    private void RenewUI(RoleData r)
    {
        if(r.unlock == 0)
        {
            RoleSelectPanel.instance.roleName.text = "？？？";
            RoleSelectPanel.instance.roleDescribe.text = r.unlockConditions;
            RoleSelectPanel.instance.roleRecord.text = "尚无记录";
            RoleSelectPanel.instance.avatar.sprite = Resources.Load<Sprite>("Image/UI/锁");
        }
        else
        {
            RoleSelectPanel.instance.roleName.text = r.name;
            RoleSelectPanel.instance.roleDescribe.text = r.describe;
            RoleSelectPanel.instance.roleRecord.text = GetRecord(r.record);
            RoleSelectPanel.instance.avatar.sprite = Resources.Load<Sprite>(r.avatar);
        }
    }

    private string GetRecord(int record)
    {
        switch(record)
        {
            case -1:
                return "尚无记录";
            case 0:
                return "通关简单难度";
            case 1:
                return "通关普通难度";
            case 2:
                return "通关困难难度";
            case 3:
                return "通关极难难度";
            case 4:
                return "通关地狱难度";
            case 5:
                return "通关末日难度";
            default:
                return "";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = new Color(34 / 255f, 34 / 255f, 34 / 255f);
    }
}
