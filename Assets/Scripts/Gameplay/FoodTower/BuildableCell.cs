using UnityEngine;

public class BuildableCell : MonoBehaviour
{
    // 标记该单元格是否已经有建筑
    private bool isOccupied = false;

    // 当有物体尝试在该单元格上建造时调用的方法
    public bool TryBuild(GameObject towerPrefab)
    {
        if (isOccupied)
        {
            // 如果单元格已经被占用，返回 false 表示建造失败
            Debug.Log("该单元格已经被占用，无法建造！");
            return false;
        }

        // 在该单元格的位置实例化 FoodTower
        if (towerPrefab != null)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity, transform);
            // 标记该单元格已被占用
            isOccupied = true;
            return true;
        }
        else
        {
            Debug.LogError("FoodTower 预制体为空，无法创建！");
            return false;
        }
    }

    // 当该单元格上的建筑被移除时调用的方法
    public void ClearCell()
    {
        // 标记该单元格为空
        isOccupied = false;
    }
}