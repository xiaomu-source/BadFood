using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FoodTower : MonoBehaviour
{
    public GameObject foodPrefab;
    public int id;
    public float interval = 3f;
    public string towerName;

    public string buildCoins;

    // public string image;
    public int foodId;
    public float calCur;

    // 用于显示顾客图像的Image组件
    private Image towerImage;

    void Start()
    {
        // towerImage = GetComponentInChildren<Image>();
        // if (towerImage == null)
        // {
        //     Debug.LogError("未找到用于显示食物塔图像的Image组件，请确保食物塔图像对象下有Image子对象。");
        // }
        // else
        // {
        //     // 从指定路径加载顾客图像
        //     LoadTowerImage();
        // }

        StartCoroutine(GenerateFoodRoutine());
    }

    // private void LoadTowerImage()
    // {
    //     Sprite towerSprite = Resources.Load<Sprite>(image);
    //     if (towerSprite != null)
    //     {
    //         towerImage.sprite = towerSprite;
    //     }
    //     else
    //     {
    //         Debug.LogError($"未能找到食物塔图像，路径：{image}");
    //     }
    // }

    IEnumerator GenerateFoodRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GenerateFood();
        }
    }

    void GenerateFood()
    {
        if (foodPrefab == null) return;

        // 找到最近的顾客
        Customer nearestCustomer = FindNearestCustomer();
        if (nearestCustomer == null) return;

        // 生成食物对象
        GameObject foodObject = Instantiate(foodPrefab, transform.position, Quaternion.identity, transform);
        Food food = foodObject.GetComponent<Food>();

        // 设置食物的目标顾客
        food.SetTarget(nearestCustomer.transform);
    }

    private Customer FindNearestCustomer()
    {
        Customer[] allCustomers = FindObjectsOfType<Customer>();
        if (allCustomers.Length == 0) return null;

        Customer nearestCustomer = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Customer customer in allCustomers)
        {
            float distance = Vector3.Distance(transform.position, customer.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestCustomer = customer;
            }
        }

        return nearestCustomer;
    }
}