using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectPanel : MonoBehaviour
{
    public static RoleSelectPanel instance;

    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset;

    public Transform roleList;
    public GameObject rolePrefabs;
    public TextMeshProUGUI roleName;
    public TextMeshProUGUI roleDescribe;
    public TextMeshProUGUI roleRecord;
    public Image avatar;
    public CanvasGroup canvasGroup;
    public GameObject roleDetail;

    private void Awake()
    {
        instance = this;

        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);

        roleList = GameObject.Find("RoleList").transform;
        rolePrefabs = Resources.Load<GameObject>("prefabs/Role");

        roleName = GameObject.Find("RoleName").GetComponent<TextMeshProUGUI>();
        roleDescribe = GameObject.Find("RoleDescribe").GetComponent<TextMeshProUGUI>();
        roleRecord = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();
        avatar =  GameObject.Find("Avatar_Role").GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        roleDetail = GameObject.Find("RoleDetails");
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (RoleData roleData in roleDatas)
        {
            RoleUI roleUI = Instantiate(rolePrefabs, roleList).GetComponent<RoleUI>();
            roleUI.SetData(roleData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
