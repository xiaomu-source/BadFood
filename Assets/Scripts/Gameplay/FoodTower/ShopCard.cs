using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // 对应的 FoodTower 预制体列表
    // public GameObject foodTowerPrefab;
    public GameObject[] foodTowerPrefabs;

    // 当前选择的 FoodTower 预制体
    private GameObject selectedFoodTowerPrefab;

    // 用于显示 FoodTower 图片的 Image 组件
    private Image foodTowerImage;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    void Awake()
    {
        // // 检查 Game.GlobalFoodTowers 是否为空
        // if (Game.GlobalFoodTowers == null || Game.GlobalFoodTowers.foodTowers == null ||
        //     Game.GlobalFoodTowers.foodTowers.Count == 0)
        // {
        //     Debug.LogWarning("Game.GlobalFoodTowers 为空，无法生成食物塔！");
        //     return;
        // }
        //
        // // 遍历 Game.GlobalFoodTowers 列表，生成每个食物塔
        // for (int i = 0; i < Game.GlobalFoodTowers.foodTowers.Count; i++)
        // {
        //     SpawnFoodTower(Game.GlobalFoodTowers.foodTowers[i]);
        // }

        rectTransform = GetComponent<RectTransform>();
        // 检查 CanvasGroup 组件是否存在
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            // 如果不存在，尝试添加 CanvasGroup 组件
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 检查 foodTowerImage 是否存在
        if (foodTowerImage == null)
        {
            foodTowerImage = GetComponent<Image>();
            if (foodTowerImage == null)
            {
                Debug.LogError("ShopCard 上没有找到 Image 组件！");
            }
        }

        // 随机选择一个 FoodTower 预制体
        if (foodTowerPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, foodTowerPrefabs.Length);
            selectedFoodTowerPrefab = foodTowerPrefabs[randomIndex];

            // 设置 FoodTower 的图片
            if (selectedFoodTowerPrefab != null)
            {
                SpriteRenderer spriteRenderer = selectedFoodTowerPrefab.GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    foodTowerImage.sprite = spriteRenderer.sprite;
                }
            }
        }
        else
        {
            Debug.LogError("foodTowerPrefabs 列表为空！");
        }
    }

    void Start()
    {
        if (foodTowerImage == null)
        {
            foodTowerImage = GetComponent<Image>();
            if (foodTowerImage == null)
            {
                Debug.LogError("ShopCard 上没有找到 Image 组件！");
            }
            else
            {
                Debug.Log("成功获取到 Image 组件！");
            }
        }

        // 如果还没有选择 FoodTower 预制体，再次随机选择一个
        if (selectedFoodTowerPrefab == null && foodTowerPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, foodTowerPrefabs.Length);
            selectedFoodTowerPrefab = foodTowerPrefabs[randomIndex];
        }

        // 设置 FoodTower 的图片
        if (selectedFoodTowerPrefab != null)
        {
            SpriteRenderer spriteRenderer = selectedFoodTowerPrefab.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                foodTowerImage.sprite = spriteRenderer.sprite;
                Debug.Log("成功设置图片！");
            }
            else
            {
                Debug.LogError("FoodTower 预制体中没有找到 SpriteRenderer 组件！");
            }
        }
        else
        {
            Debug.LogError("foodTowerPrefabs 列表为空，无法选择预制体！");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true;
        }

        // 获取鼠标点击位置的所有碰撞对象
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            // 检查是否拖到了 BuildableCell(Clone) 上
            if (result.gameObject.name.Contains("BuildableCell(Clone)"))
            {
                // 获取 BuildableCell 组件
                BuildableCell buildableCell = result.gameObject.GetComponent<BuildableCell>();
                if (buildableCell != null)
                {
                    if (buildableCell.TryBuild(selectedFoodTowerPrefab))
                    {
                        // 建造成功
                        Debug.Log("FoodTower 创建成功");
                        break;
                    }
                }
            }
        }

        // 重置卡片位置
        rectTransform.anchoredPosition = Vector2.zero;
    }

    // void SpawnFoodTower(FoodTowerModel foodTowerModel)
    // {
    //     if (Game.GlobalPathPoints.Count < 2) return;
    //
    //     // 实例化食物塔预制体
    //     GameObject foodTower = Instantiate(
    //         foodTowerPrefab,
    //         Game.GlobalPathPoints[0],
    //         Quaternion.identity,
    //         transform
    //     );
    //
    //     // 获取食物塔脚本组件
    //     FoodTower foodTowerScript = foodTower.GetComponent<FoodTower>();
    //     if (foodTowerScript != null)
    //     {
    //         // 初始化食物塔数据
    //         foodTowerScript.id = foodTowerModel.id;
    //         foodTowerScript.towerName = foodTowerModel.name;
    //         foodTowerScript.buildCoins = foodTowerModel.buildCoins;
    //         foodTowerScript.image = foodTowerModel.image;
    //         foodTowerScript.foodId = foodTowerModel.foodId;
    //         foodTowerScript.interval = foodTowerModel.interval;
    //         foodTowerScript.calCur = foodTowerModel.calCur;
    //     }
    //     else
    //     {
    //         Debug.LogError("FoodTower 组件未找到！");
    //     }
    // }
}