using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;

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

    public GameObject[] male;
    public GameObject[] feMale;
    int characterIndex;

    Vector2 corner1 = new Vector2(5f, 4f);
    Vector2 corner2 = new Vector2(5f, -6f);
    Vector2 corner3 = new Vector2(-7f, -6f);

    bool canWin = true;

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

        SpawnPlayer();
        if (index < 0 || index >= dataMaps.Length) return;
        DataMap dataMap = dataMaps[index];

        if (dataMap.prefabMap != null)
        {
            GameObject map = Instantiate(dataMap.prefabMap, new Vector2(-0.5f, -1.5f), Quaternion.identity);
        }
        else Debug.LogError("Prefab map chưa được gán trong DataMap.");

        LoadEnemy(index);
        level.text = (index + 1).ToString();
    }

    void SpawnPlayer()
    {
        int i = PlayerPrefs.GetInt("male");
        if (i == 1)
        {
            characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
            GameObject player = Instantiate(male[characterIndex], new Vector2(-7, 5), Quaternion.identity);
        }
        else
        {
            characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
            GameObject player = Instantiate(feMale[characterIndex], new Vector2(-7, 5), Quaternion.identity);
        }
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

        if (currentLevel.enemies.Count >= 3)
        {
            GameObject enemy1 = Instantiate(currentLevel.enemies[0], corner1, Quaternion.identity);
            GameObject enemy2 = Instantiate(currentLevel.enemies[1], corner2, Quaternion.identity);
            GameObject enemy3 = Instantiate(currentLevel.enemies[2], corner3, Quaternion.identity);
        }
        else if (currentLevel.enemies.Count == 1)
        {
            GameObject enemy = Instantiate(currentLevel.enemies[0], corner1, Quaternion.identity);
        }
        else
            Debug.Log("Not enough enemies in the level to instantiate at all corners.");
        countEnemy = currentLevel.enemies.Count;
    }

    public void OnEnemyDestroyed()
    {
        countEnemy--;
        if (canWin)
        {
            if (countEnemy <= 0) if (panelWin != null) panelWin.PanelFadeIn();
        }
    }
    public void Replay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void NextLevel()
    {
        numberSelect++;
        if (numberSelect > numberLevel) numberLevel++;
        else numberLevel = numberSelect;
        SceneManager.LoadScene("GamePlay");

        PlayerPrefs.SetInt("SelectedLevel", numberSelect);
        PlayerPrefs.Save();

        if (numberLevel >= numberSelect)
        {
            PlayerPrefs.SetInt("CompletedLevel", numberLevel);
            PlayerPrefs.Save();
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
}
