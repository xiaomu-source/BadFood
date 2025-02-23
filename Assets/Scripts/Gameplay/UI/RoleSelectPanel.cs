using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectPanel : MonoBehaviour
{
    public static RoleSelectPanel Instance;
    
    public List<RoleData> roleDatas = new List<RoleData>();
    public TextAsset roleTextAsset; // json文件

    public Transform _roleList; //角色列表 UI
    public GameObject role_prefab; //角色预制体
    
    public TextMeshProUGUI _roleName; //角色名称
    public Image _avatar; //角色头像
    public TextMeshProUGUI _roleDescribe; //角色描述
    public TextMeshProUGUI _text3; //通过记录
    

    private void Awake()
    {
        Instance = this;
        
        _roleList = GameObject.Find("RoleList").transform;
        role_prefab = Resources.Load<GameObject>("Prefabs/Role");
        
        // 读取json文件， 并转化为对象
        roleTextAsset = Resources.Load<TextAsset>("Data/role");
        roleDatas = JsonConvert.DeserializeObject<List<RoleData>>(roleTextAsset.text);
        
        _roleName = GameObject.Find("RoleName").GetComponent<TextMeshProUGUI>();
        _avatar = GameObject.Find("Avatar_Role").GetComponent<Image>();
        _roleDescribe = GameObject.Find("RoleDescribe").GetComponent<TextMeshProUGUI>();
        _text3 = GameObject.Find("Text3").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (RoleData roleData in roleDatas)
        {
            RoleUI r = GameObject.Instantiate(role_prefab, _roleList).GetComponent<RoleUI>();
            r.SetData(roleData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
