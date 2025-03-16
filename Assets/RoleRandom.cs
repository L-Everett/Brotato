using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleRandom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;
    public Image bg;
    public List<RoleUI> unlockRoles = new List<RoleUI>();

    public void OnPointerEnter(PointerEventData eventData)
    {
        bg.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        RoleSelectPanel.instance.avatar.sprite = Resources.Load<Sprite>("Image/UI/ÎÊºÅ");
        RoleSelectPanel.instance.roleName.text = "???";
        RoleSelectPanel.instance.roleDescribe.text = "Ëæ»úÑ¡Ôñ";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.color = new Color(37 / 255f, 37 / 255f, 37 / 255f);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
        bg = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            foreach (RoleUI role in RoleSelectPanel.instance.roleList.GetComponentsInChildren<RoleUI>())
            {
                if (role.roleData.unlock == 1)
                {
                    unlockRoles.Add(role);
                }
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
