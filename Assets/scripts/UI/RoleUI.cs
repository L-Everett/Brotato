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
            avatar.sprite = Resources.Load<Sprite>("Image/UI/��");
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
        //�����ɫ��Ϣ
        GameManager.Instance.RoleData = roleData;

        //�رս�ɫѡ�����
        RoleSelectPanel.instance.canvasGroup.alpha = 0;
        RoleSelectPanel.instance.canvasGroup.interactable = false;
        RoleSelectPanel.instance.canvasGroup.blocksRaycasts = false;

        //���ƽ�ɫUI
        GameObject roleDetail = Instantiate(RoleSelectPanel.instance.roleDetail, WeaponSelectPanel.instance.weaponCotent);
        roleDetail.transform.SetSiblingIndex(0);

        //������ѡ�����
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
            RoleSelectPanel.instance.roleName.text = "������";
            RoleSelectPanel.instance.roleDescribe.text = r.unlockConditions;
            RoleSelectPanel.instance.roleRecord.text = "���޼�¼";
            RoleSelectPanel.instance.avatar.sprite = Resources.Load<Sprite>("Image/UI/��");
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
                return "���޼�¼";
            case 0:
                return "ͨ�ؼ��Ѷ�";
            case 1:
                return "ͨ����ͨ�Ѷ�";
            case 2:
                return "ͨ�������Ѷ�";
            case 3:
                return "ͨ�ؼ����Ѷ�";
            case 4:
                return "ͨ�ص����Ѷ�";
            case 5:
                return "ͨ��ĩ���Ѷ�";
            default:
                return "";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = new Color(34 / 255f, 34 / 255f, 34 / 255f);
    }
}
