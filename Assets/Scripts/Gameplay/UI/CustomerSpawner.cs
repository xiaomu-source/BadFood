using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public float spawnInterval = 5f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCustomer()
    {
        if (Game.GlobalPathPoints.Count < 2) return;
        if (Game.GlobalCustomers == null || Game.GlobalCustomers.customers == null ||
            Game.GlobalCustomers.customers.Count == 0) return;

        // 随机选择一个 CustomerData
        int randomIndex = Random.Range(0, Game.GlobalCustomers.customers.Count);
        CustomerModel customerModel = Game.GlobalCustomers.customers[randomIndex];

        // 实例化顾客预制体
        GameObject customer = Instantiate(
            customerPrefab,
            Game.GlobalPathPoints[0],
            Quaternion.identity,
            transform
        );

        // 获取顾客脚本组件
        Customer customerScript = customer.GetComponent<Customer>();
        if (customerScript != null)
        {
            // 初始化顾客数据
            customerScript.id = customerModel.id;
            customerScript.customerName = customerModel.name;
            customerScript.coins = customerModel.coins;
            customerScript.image = customerModel.image;
            customerScript.calCur = customerModel.calCur;
            customerScript.calReq = customerModel.calReq;
            customerScript.speed = customerModel.speed;

            // 设置顾客的移动路径
            customerScript.SetPath(Game.GlobalPathPoints);
        }
        else
        {
            Debug.LogError("Customer 组件未找到！");
        }
    }
}