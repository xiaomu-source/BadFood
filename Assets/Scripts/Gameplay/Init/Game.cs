using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static List<Vector3> GlobalPathPoints = new List<Vector3>();
    public static List<Vector3> GlobalBuildablePoints = new List<Vector3>();
    public static List<Vector3> GlobalEntrancePoints = new List<Vector3>();
    public static List<Vector3> GlobalShopPoints = new List<Vector3>();
    public static List<Vector3> GlobalEquipmentStoragePoints = new List<Vector3>();
    public static List<Vector3> GlobalShopButtonPoints = new List<Vector3>();
    public static CustomerData GlobalCustomers;
    public static FoodTowerData GlobalFoodTowers;
    // 将加载的数据存为成员变量
    private MapData _mapData;

    void Start()
    {
        //确保 Game对象一直存在
        Object.DontDestroyOnLoad(this.gameObject);

        LoadMapData("Data/map");
        LoadCustomerData("Data/customers");
        LoadFoodTowerData("Data/foodTowers");
        
        SceneManager.LoadScene("03-GamePlay");
    }
    
    private void LoadFoodTowerData(string fileName)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(fileName);

        if (jsonData != null)
        {
            GlobalFoodTowers = JsonConvert.DeserializeObject<FoodTowerData>(jsonData.text);
        }
        else
        {
            Debug.LogError("Failed to load customer data from Resources.");
        }
    }

    private void LoadCustomerData(string fileName)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(fileName);

        if (jsonData != null)
        {
            GlobalCustomers = JsonConvert.DeserializeObject<CustomerData>(jsonData.text);
        }
        else
        {
            Debug.LogError("Failed to load customer data from Resources.");
        }
    }

    void LoadMapData(string fileName)
    {
        TextAsset jsonData = Resources.Load<TextAsset>(fileName);

        if (jsonData != null)
        {
            _mapData = JsonConvert.DeserializeObject<MapData>(jsonData.text);
            LoadMapPoints(_mapData);
        }
        else
        {
            Debug.LogError("Failed to load map data from Resources.");
        }
    }

    void LoadMapPoints(MapData data)
    {
        foreach (CellData cell in data.cells)
        {
            Vector3 worldPos = GetWorldPosition(cell.GetCenterPosition());

            switch (cell.cellType)
            {
                case CellData.CellType.Path:
                    GlobalPathPoints.Add(worldPos);
                    break;
                case CellData.CellType.Entrance:
                    GlobalEntrancePoints.Add(worldPos);
                    break;
                case CellData.CellType.Buildable:
                    GlobalBuildablePoints.Add(worldPos);
                    break;
                case CellData.CellType.Shop:
                    GlobalShopPoints.Add(worldPos);
                    break;
                case CellData.CellType.ShopButton:
                    GlobalShopButtonPoints.Add(worldPos);
                    break;
                case CellData.CellType.EquipmentStorage:
                    GlobalEquipmentStoragePoints.Add(worldPos);
                    break;
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
}