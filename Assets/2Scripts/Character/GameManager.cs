using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numberLevel;
    private int numberSelect;
    private int countEnemy = 0;
    private int characterIndex;

    public GameObject[] male;
    public GameObject[] feMale;

    public DataMap[] dataMaps;
    public bool isPause = true;
    private bool canWin = true;
    public ListEnemy[] listEnemy;
    public TextMeshProUGUI level;
    private UiPanelDotween panelWin;
    public UiPanelDotween panelLose;
    Vector2 corner1 = new Vector2(5f, 4f);
    Vector2 corner2 = new Vector2(5f, -6f);
    Vector2 corner3 = new Vector2(-7f, -6f);

    public SceneFader sceneFader;
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
        LoadEnemy(index);
        level.text = (index + 1).ToString();
    }

    void SpawnPlayer()
    {
        int i = PlayerPrefs.GetInt("male");
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject[] characterArray = (i == 1) ? male : feMale;
        GameObject player = Instantiate(characterArray[characterIndex], new Vector2(-7, 5), Quaternion.identity);
    }

    void LoadReSoure()
    {
        listEnemy = Resources.LoadAll<ListEnemy>("ListEnemy");
        dataMaps = Resources.LoadAll<DataMap>("Map");
    }

    void LoadEnemy(int levelIndex)
    {
        if (listEnemy == null || listEnemy.Length <= levelIndex) return;
        ListEnemy currentLevel = listEnemy[levelIndex];
        Vector2[] corners = { corner1, corner2, corner3 };
        for (int i = 0; i < currentLevel.enemies.Count && i < corners.Length; i++)
        {
            Instantiate(currentLevel.enemies[i], corners[i], Quaternion.identity);
        }
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
        sceneFader.FadeTo("GamePlay");
    }

    public void NextLevel()
    {
        numberSelect++;
        if (numberSelect > numberLevel) numberLevel++;
        else numberLevel = numberSelect;
        sceneFader.FadeTo("GamePlay");
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
