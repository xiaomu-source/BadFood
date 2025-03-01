using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform canvasTransform;
    public GameObject pathPrefab;
    public GameObject buildablePrefab;
    public GameObject entrancePrefab;
    public GameObject shopPrefab;
    public GameObject equipmentStoragePrefab;
    public GameObject shopButtonPrefab;

    // 用于控制是否显示辅助线
    public bool drawGizmos = true;

    // 最小单元格尺寸
    public int cellSize = 64;

    // 将加载的数据存为成员变量，方便 OnDrawGizmos 中访问
    private MapData _mapData;

    // 四个分组对象
    private Transform pathEntranceGroup;
    private Transform buildableGroup;
    private Transform shopGroup;
    private Transform shopButtonGroup;
    private Transform equipmentStorageGroup;

    void Start()
    {
        // 创建四个分组对象
        CreateGroups();
        // 将 pathEntranceGroup 对象设为不显示
        HidePathEntranceGroup();
        // HideBuildableGroup();
        HideShopGroup();
        HideShopButtonGroup();
        GenerateMap();
    }

    // 新增的 HidePathEntranceGroup 方法
    private void HidePathEntranceGroup()
    {
        if (pathEntranceGroup != null)
        {
            pathEntranceGroup.gameObject.SetActive(false);
        }
    }

    // 新增的 HideBuildableGroup 方法
    private void HideBuildableGroup()
    {
        if (buildableGroup != null)
        {
            buildableGroup.gameObject.SetActive(false);
        }
    }

    // 新增的 HideShopGroup 方法
    private void HideShopGroup()
    {
        if (shopGroup != null)
        {
            shopGroup.gameObject.SetActive(false);
        }
    }

    private void HideShopButtonGroup()
    {
        if (shopButtonGroup != null)
        {
            shopButtonGroup.gameObject.SetActive(false);
        }
    }

    void CreateGroups()
    {
        pathEntranceGroup = CreateGroupObject("PathEntranceGroup");
        buildableGroup = CreateGroupObject("BuildableGroup");
        shopGroup = CreateGroupObject("ShopGroup");
        equipmentStorageGroup = CreateGroupObject("EquipmentStorageGroup");
        shopButtonGroup = CreateGroupObject("shopButtonGroup");
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

    void GenerateMap()
    {
        InstantiateObjects(Game.GlobalPathPoints, CellData.CellType.Path);
        InstantiateObjects(Game.GlobalBuildablePoints, CellData.CellType.Buildable);
        InstantiateObjects(Game.GlobalEntrancePoints, CellData.CellType.Entrance);
        InstantiateObjects(Game.GlobalShopPoints, CellData.CellType.Shop);
        InstantiateObjects(Game.GlobalEquipmentStoragePoints, CellData.CellType.EquipmentStorage);
        InstantiateObjects(Game.GlobalShopButtonPoints, CellData.CellType.ShopButton);
    }

    void InstantiateObjects(List<Vector3> points, CellData.CellType cellType)
    {
        foreach (Vector3 point in points)
        {
            GameObject prefab = GetPrefabByType(cellType);
            Vector3 rawPoint = new Vector3(point.x, point.y - 1080, point.z);
            if (prefab != null)
            {
                Transform parentGroup = GetParentGroup(cellType);
                // 将预制体实例化为对应分组的子对象
                GameObject instance = Instantiate(prefab, parentGroup);
                // 对于 UI 元素，通常设置 localPosition 或 anchoredPosition 更合适
                RectTransform rt = instance.GetComponent<RectTransform>();
                if (rt != null)
                {
                    rt.anchoredPosition = rawPoint;
                }
                else
                {
                    instance.transform.localPosition = new Vector3(rawPoint.x, rawPoint.y, 0);
                }
            }
        }
    }

    // 根据单元格类型获取对应的分组对象
    Transform GetParentGroup(CellData.CellType type)
    {
        return type switch
        {
            CellData.CellType.Path or CellData.CellType.Entrance => pathEntranceGroup,
            CellData.CellType.Buildable => buildableGroup,
            CellData.CellType.Shop => shopGroup,
            CellData.CellType.ShopButton => shopButtonGroup,
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
        for (int x = 0; x < 30; x++)
        {
            Gizmos.DrawLine(new Vector3(x * cellSize, 0, 0), new Vector3(x * cellSize, 1080, 0));
        }

        // 绘制水平辅助线
        for (int x = 0; x < 16; x++)
        {
            Gizmos.DrawLine(new Vector3(0, 56 + x * cellSize, 0), new Vector3(1920, 56 + x * cellSize, 0));
        }

        // 当数据已经加载后，绘制每个单元格的边界（假设每个格子的大小为 64x64）
        if (_mapData != null && _mapData.cells != null)
        {
            Gizmos.color = Color.green;
            foreach (CellData cell in _mapData.cells)
            {
                // 计算中心位置
                Vector2 center = cell.GetCenterPosition();
                Vector3 pos = new Vector3(center.x, center.y + 1080, 0);
                // 绘制一个以中心点为中心、大小为 64x64 的线框矩形
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
            CellData.CellType.ShopButton => shopButtonPrefab,
            _ => null
        };
    }
}