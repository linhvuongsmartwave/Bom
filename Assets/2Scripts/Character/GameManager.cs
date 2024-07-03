using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public DataMap[] dataMaps;
    public ListEnemy[] listEnemy;
    private int numberSelect;
    private int numberLevel;
    public TextMeshProUGUI level;
    public int countEnemy = 0;
    private UiPanelDotween panelWin;
    public UiPanelDotween panelLose;
    public bool isPause = true;

    public GameObject[] playerPrefabs;
    int characterIndex;

    Vector2 corner1 = new Vector2(5f, 4f);
    Vector2 corner2 = new Vector2(5f, -6f);
    Vector2 corner3 = new Vector2(-7f, -6f);

    public List<GameObject> listE = new List<GameObject>();
    public List<GameObject> listM = new List<GameObject>();
    public List<GameObject> listP = new List<GameObject>();

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        numberSelect = PlayerPrefs.GetInt("SelectedLevel", 0);
        numberLevel = PlayerPrefs.GetInt("CompletedLevel", 0);
    }

    void Start()
    {
        LoadReSoure();
        if (dataMaps != null && dataMaps.Length > 0) LoadMap(numberSelect);
        else Debug.LogError("Cannot load data");

        panelWin = GameObject.Find(Const.panelWin).GetComponent<UiPanelDotween>();
        panelLose = GameObject.Find(Const.panelLose).GetComponent<UiPanelDotween>();
    }

    void LoadMap(int index)
    {
        //Clear();
        SpawnPlayer();
        if (index < 0 || index >= dataMaps.Length) return;
        DataMap dataMap = dataMaps[index];

        if (dataMap.prefabMap != null)
        {
            GameObject map = Instantiate(dataMap.prefabMap, new Vector2(-0.5f, -1.5f), Quaternion.identity);
            listM.Add(map);
        }
        else Debug.LogError("Prefab map chưa được gán trong DataMap.");

        LoadEnemy(index);
        level.text = (index + 1).ToString();
    }

    void SpawnPlayer()
    {
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerPrefabs[characterIndex], new Vector2(-7, 5), Quaternion.identity);
        listP.Add(player);
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
        countEnemy = currentLevel.enemies.Count;

        if (currentLevel.enemies.Count >= 3)
        {
            if (listE.Count > 0)
            {
                return;
            }
            else
            {

                GameObject enemy1 = Instantiate(currentLevel.enemies[0], corner1, Quaternion.identity);
                GameObject enemy2 = Instantiate(currentLevel.enemies[1], corner2, Quaternion.identity);
                GameObject enemy3 = Instantiate(currentLevel.enemies[2], corner3, Quaternion.identity);
                listE.Add(enemy1);
                listE.Add(enemy2);
                listE.Add(enemy3);
            }
        }
        else if (currentLevel.enemies.Count == 1)
        {
            GameObject enemy = Instantiate(currentLevel.enemies[0], corner1, Quaternion.identity);
            listE.Add(enemy);
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
            if (panelWin != null)
            {
                panelWin.PanelFadeIn();
                RfHolder.Instance.IconTrue();
            }
        }
    }

    public void Pause()
    {
        isPause = false;
    }

    public void Resume()
    {
        isPause = true;
    }

    private void Update()
    {
        Debug.Log("numberSelect: " + numberSelect);
        Debug.Log("numberLevel: " + numberLevel);
    }

    public void NextLevel()
    {
        Clear();
        numberSelect++;
        if (numberSelect > numberLevel) numberLevel++;
        else
        {
            numberLevel = numberSelect++;
        }
        LoadMap(numberLevel);
        if (numberLevel >= numberSelect)
        {
            PlayerPrefs.SetInt("CompletedLevel", numberLevel);
            PlayerPrefs.Save();
        }
        RfHolder.Instance.FindBomControl();
    }

    public void Replay()
    {
        Clear();
        LoadMap(numberSelect);
        RfHolder.Instance.FindBomControl();

    }

    public void Clear()
    {
        foreach (GameObject obj in listP)
        {
            Destroy(obj);
        }
        //if (listE.Count >0)
        //{
        //    foreach (GameObject obj in listE)
        //    {
        //        obj.SetActive(false);
        //    }
        //}
        foreach (GameObject obj in listM)
        {
            Destroy(obj);

        }

        listE.Clear();
        listM.Clear();
        listP.Clear();
    }
}
