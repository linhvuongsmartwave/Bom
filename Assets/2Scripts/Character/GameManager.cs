using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DataMap[] dataMaps;
    public int levelStart;
    public LevelData level1;
    public int count = 0;
    Vector2 corner1 = new Vector2(5f, 5f);
    Vector2 corner2 = new Vector2(5f, -5f);
    Vector2 corner3 = new Vector2(-7f, -5f);



    void Start()
    {
        if (dataMaps != null && dataMaps.Length > 0) LoadMap(levelStart);
        else Debug.LogError("dont can load data");

        count = level1.enemies.Count;
        if (levelStart < 5)
        {
            Instantiate(level1.enemies[0], corner1, Quaternion.identity);
            Instantiate(level1.enemies[1], corner2, Quaternion.identity);
            Instantiate(level1.enemies[2], corner3, Quaternion.identity);

        }

    }


    void LoadMap(int index)
    {
        if (index < 0 || index >= dataMaps.Length) return;
        DataMap dataMap = dataMaps[index];

        if (dataMap.prefabMap != null)
            Instantiate(dataMap.prefabMap, new Vector2(-0.5f, -0.5f), Quaternion.identity);

        else Debug.LogError("Prefab map chưa được gán trong DataMap.");
    }

    public void NextLevel()
    {
        int nextLevel = levelStart + 1;
        LoadMap(nextLevel);
    }
}
