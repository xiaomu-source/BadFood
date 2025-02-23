using UnityEngine;

[System.Serializable]
public class CellData
{
    public string id;
    public Vector2Int[] coordinates; // 存储单元格四个坐标点
    public CellType cellType;

    public enum CellType
    {
        Path,
        Buildable,
        Entrance,
        Shop,
        EquipmentStorage
    }

    // 计算中心点
    public Vector2 GetCenterPosition()
    {
        if (coordinates.Length != 4)
        {
            Debug.LogError("Invalid cell coordinates");
            return Vector2.zero;
        }
        return new Vector2(
            coordinates[0].x * 60f,  // 转换为像素坐标
            -coordinates[0].y * 60f
        );
    }
}