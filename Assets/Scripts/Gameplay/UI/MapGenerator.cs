using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class MapGenerator : MonoBehaviour
{
    public Transform canvasTransform;
    public GameObject pathPrefab;
    public GameObject buildablePrefab;
    public GameObject entrancePrefab;
    public GameObject shopPrefab;
    public GameObject equipmentStoragePrefab;

    public static List<Vector3> GlobalPathPoints = new List<Vector3>();

    // 用于控制是否显示辅助线
    public bool drawGizmos = true;

    // 最小单元格尺寸
    public int cellSize = 60;

    // 将加载的数据存为成员变量，方便 OnDrawGizmos 中访问
    private LevelData levelData;

    // 四个分组对象
    private Transform pathEntranceGroup;
    private Transform buildableGroup;
    private Transform shopGroup;
    private Transform equipmentStorageGroup;

    void Start()
    {
        // 创建四个分组对象
        CreateGroups();
        LoadLevelData("Data/map1");
    }

    void CreateGroups()
    {
        pathEntranceGroup = CreateGroupObject("PathEntranceGroup");
        buildableGroup = CreateGroupObject("BuildableGroup");
        shopGroup = CreateGroupObject("ShopGroup");
        equipmentStorageGroup = CreateGroupObject("EquipmentStorageGroup");
    }

    Transform CreateGroupObject(string groupName)
    {
        GameObject groupObj = new GameObject(groupName);
        RectTransform rectTransform = groupObj.AddComponent<RectTransform>();
        rectTransform.SetParent(transform);

        // 设置锚点为左上角
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchoredPosition = Vector2.zero;

        // 设置对象的宽高为 1920, 1080
        rectTransform.sizeDelta = new Vector2(1920, 1080);

        return groupObj.transform;
    }

    void LoadLevelData(string fileName)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(fileName);

        if (jsonData != null)
        {
            levelData = JsonConvert.DeserializeObject<LevelData>(jsonData.text);
            GenerateMap(levelData);
        }
        else
        {
            Debug.LogError("Failed to load map data from Resources.");
        }
    }

    void GenerateMap(LevelData data)
    {
        GlobalPathPoints.Clear();

        foreach (CellData cell in data.cells)
        {
            if (cell.cellType == CellData.CellType.Path ||
                cell.cellType == CellData.CellType.Entrance)
            {
                Vector3 worldPos = GetWorldPosition(cell.GetCenterPosition());
                GlobalPathPoints.Add(worldPos);
            }

            Vector2 position = cell.GetCenterPosition();
            GameObject prefab = GetPrefabByType(cell.cellType);

            if (prefab != null)
            {
                Transform parentGroup = GetParentGroup(cell.cellType);
                // 将预制体实例化为对应分组的子对象
                GameObject instance = Instantiate(prefab, parentGroup);
                // 对于 UI 元素，通常设置 localPosition 或 anchoredPosition 更合适
                RectTransform rt = instance.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = position;
                }
                else
                {
                    instance.transform.localPosition = new Vector3(position.x, position.y, 0);
                }
            }
        }
    }

    private Vector3 GetWorldPosition(Vector2 rawPosition)
    {
        return new Vector3(
            rawPosition.x,
            rawPosition.y + 1080,
            0
        );
    }

    // 根据单元格类型获取对应的分组对象
    Transform GetParentGroup(CellData.CellType type)
    {
        return type switch
        {
            CellData.CellType.Path or CellData.CellType.Entrance => pathEntranceGroup,
            CellData.CellType.Buildable => buildableGroup,
            CellData.CellType.Shop => shopGroup,
            CellData.CellType.EquipmentStorage => equipmentStorageGroup,
            _ => null
        };
    }

    // 使用 Gizmos 绘制辅助线
    void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        // 设置网格线颜色为浅灰色
        Gizmos.color = new Color(0.8f, 0.8f, 0.8f, 0.1f);

        // 垂直辅助线
        for (int x = 0; x < 32; x++)
        {
            Gizmos.DrawLine(new Vector3(x * cellSize, 0, 0), new Vector3(x * cellSize, 1080, 0));
        }

        // 绘制水平辅助线
        for (int x = 0; x < 18; x++)
        {
            Gizmos.DrawLine(new Vector3(0, x * cellSize, 0), new Vector3(1920, x * cellSize, 0));
        }

        // 当数据已经加载后，绘制每个单元格的边界（假设每个格子的大小为 60x60）
        if (levelData != null && levelData.cells != null)
        {
            Gizmos.color = Color.green;
            foreach (CellData cell in levelData.cells)
            {
                // 计算中心位置
                Vector2 center = cell.GetCenterPosition();
                Vector3 pos = new Vector3(center.x, center.y + 1080, 0);
                // 绘制一个以中心点为中心、大小为 60x60 的线框矩形
                Gizmos.DrawWireCube(pos, new Vector3(cellSize, cellSize, 0));
            }
        }
    }

    GameObject GetPrefabByType(CellData.CellType type)
    {
        return type switch
        {
            CellData.CellType.Path => pathPrefab,
            CellData.CellType.Buildable => buildablePrefab,
            CellData.CellType.Entrance => entrancePrefab,
            CellData.CellType.Shop => shopPrefab,
            CellData.CellType.EquipmentStorage => equipmentStoragePrefab,
            _ => null
        };
    }
}