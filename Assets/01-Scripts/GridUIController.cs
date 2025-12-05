using UnityEngine;

[System.Serializable]
public struct GridPresetConfig
{
    public int id;
    public int rows;
    public int columns;
}

public class GridUIController : MonoBehaviour
{
    public SpriteGridAutoFit grid;
    public GridPresetConfig[] configs;

    public void SetGrid(int id)
    {
        foreach (var config in configs)
        {
            if (config.id == id)
            {
                grid.rows = config.rows;
                grid.columns = config.columns;
                grid.RebuildGrid();
                return;
            }
        }

        Debug.LogWarning($"GridUIController: No config found for ID {id}");
    }
}
