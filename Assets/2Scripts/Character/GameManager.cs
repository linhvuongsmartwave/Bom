using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public DataMap[] dataMaps;
    public ListEnemy[] listEnemy;
    public int levelStart;
    public int countEnemy = 0;
    private UiPanelDotween panelWin;

    Vector2 corner1 = new Vector2(5f, 5f);
    Vector2 corner2 = new Vector2(5f, -5f);
    Vector2 corner3 = new Vector2(-7f, -5f);

    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadReSoure();
        if (dataMaps != null && dataMaps.Length > 0) LoadMap(levelStart);
        else Debug.LogError("dont can load data");
        panelWin = GameObject.FindObjectOfType<UiPanelDotween>();
    }
    void LoadReSoure()
    {
        listEnemy = Resources.LoadAll<ListEnemy>("ListEnemy");
        dataMaps = Resources.LoadAll<DataMap>("Map");

    }

    void LoadEnemy(int levelIndex)
    {
        if (listEnemy == null || listEnemy.Length <= levelIndex)
        {
            Debug.LogError("Invalid level index or levels not set.");
            return;
        }

        ListEnemy currentLevel = listEnemy[levelIndex];
        countEnemy=currentLevel.enemies.Count;
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
    public void OnEnemyDestroyed()
    {
        countEnemy--;
        if (countEnemy <= 0)
        {
            if(panelWin!=null) panelWin.PanelFadeIn();
        }
    }


    void LoadMap(int index)
    {
        if (index < 0 || index >= dataMaps.Length) return;
        DataMap dataMap = dataMaps[index];

        if (dataMap.prefabMap != null)
            Instantiate(dataMap.prefabMap, new Vector2(-0.5f, -0.5f), Quaternion.identity);

        else Debug.LogError("Prefab map chưa được gán trong DataMap.");

        LoadEnemy(index);
    }

    public void NextLevel()
    {
        int nextLevel = levelStart + 1;
        LoadMap(nextLevel);
    }
}
