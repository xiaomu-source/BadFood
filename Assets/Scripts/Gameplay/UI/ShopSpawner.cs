using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopSpawner : MonoBehaviour
{
    public GameObject shopPrefab;
    public GameObject shopButtonPrefab;
    private List<GameObject> spawnButtons = new List<GameObject>(); // 用于触发刷新的按钮

    private List<GameObject> spawnedShops = new List<GameObject>(); // 用于存储已生成的商店对象

    void Start()
    {
        // 开始时先自动生成一次商店
        SpawnAllShops();
        SpawnAllShopButtons();

        // 为按钮添加点击事件监听
        foreach (GameObject spawnButton in spawnButtons)
        {
            Button button = spawnButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(SpawnShops);
            }
        }
    }

    // 点击按钮时调用的方法
    public void SpawnShops()
    {
        // 重新生成所有商店
        SpawnAllShops();
    }

    private void SpawnAllShopButtons()
    {
        foreach (Vector3 shopButtonPoint in Game.GlobalShopButtonPoints)
        {
            GameObject shopButton = Instantiate(
                shopButtonPrefab,
                shopButtonPoint,
                Quaternion.identity,
                transform
            );
            spawnButtons.Add(shopButton);
        }
    }

    // 重新生成所有商店
    private void SpawnAllShops()
    {
        int shopPointIndex = 0;

        // 首先尝试重用已有的商店对象
        foreach (GameObject shop in spawnedShops)
        {
            if (shopPointIndex < Game.GlobalShopPoints.Count)
            {
                shop.transform.position = Game.GlobalShopPoints[shopPointIndex];
                shop.SetActive(true);
                shopPointIndex++;
            }
            else
            {
                shop.SetActive(false);
            }
        }

        // 如果已有对象不够用，实例化新的对象
        while (shopPointIndex < Game.GlobalShopPoints.Count)
        {
            GameObject newShop = Instantiate(
                shopPrefab,
                Game.GlobalShopPoints[shopPointIndex],
                Quaternion.identity,
                transform
            );
            spawnedShops.Add(newShop);
            shopPointIndex++;
        }
    }
}