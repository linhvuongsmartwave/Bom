using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DataMap[] dataMaps;
    public int levelStart;
    public LevelData[] levels;
    public int count = 0;
    Vector2 corner1 = new Vector2(5f, 5f);
    Vector2 corner2 = new Vector2(5f, -5f);
    Vector2 corner3 = new Vector2(-7f, -5f);



    void Start()
    {
        if (dataMaps != null && dataMaps.Length > 0) LoadMap(levelStart);
        else Debug.LogError("dont can load data");
    }

    void LoadEnemy(int levelIndex)
    {
        if (levels == null || levels.Length <= levelIndex)
        {
            Debug.LogError("Invalid level index or levels not set.");
            return;
        }

        LevelData currentLevel = levels[levelIndex];
        if (currentLevel.enemies.Count >=3)
        {
            Instantiate(currentLevel.enemies[0], corner1, Quaternion.identity);
            Instantiate(currentLevel.enemies[1], corner2, Quaternion.identity);
            Instantiate(currentLevel.enemies[2], corner3, Quaternion.identity);
        }
        else if (currentLevel.enemies.Count==1)
        {
            Instantiate(currentLevel.enemies[0], corner1, Quaternion.identity);

        }
        else
        {
            Debug.Log("Not enough enemies in the level to instantiate at all corners.");
        }
    }



    void LoadMap(int index)
    {
        if (index < 0 || index >= dataMaps.Length) return;
        DataMap dataMap = dataMaps[index];

        if (dataMap.prefabMap != null)
            Instantiate(dataMap.prefabMap, new Vector2(-0.5f, -0.5f), Quaternion.identity);

        else Debug.LogError("Prefab map chưa được gán trong DataMap.");

        if (levelStart < 5)
        {
            LoadEnemy(0);
        }
    }

    public void NextLevel()
    {
        int nextLevel = levelStart + 1;
        LoadMap(nextLevel);
    }
}
