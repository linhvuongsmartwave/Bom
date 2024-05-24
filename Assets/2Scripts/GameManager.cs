using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DataMap[] dataMaps;
    public int levelStart;

    void Start()
    {
        if (dataMaps != null && dataMaps.Length > 0)
        {
            LoadMap(levelStart); 
        }
        else
        {
            Debug.LogError("dont can load data");
        }
    }
     
    void LoadMap(int index)
    {
        if (index < 0 || index >= dataMaps.Length)
        {
            return;
        }

        DataMap dataMap = dataMaps[index];

        if (dataMap.prefabMap != null)
        {
            Instantiate(dataMap.prefabMap, new Vector2(-0.5f,-0.5f), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab map chưa được gán trong DataMap.");
        }
    }
    public void NextLevel()
    {
        LoadMap(levelStart+1);
    }
}
