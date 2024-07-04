using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public static LevelButton Instance;
    [SerializeField] private Sprite currentButton;
    [SerializeField] private Sprite lockedButton;
    [SerializeField] private Image buttonImg;
    [SerializeField] private TextMeshProUGUI txtNumberLevel;
    public SceneFader sceneFader;

    public int numLevel;
    public int nextLevel;
    private Button button;
    private bool canClick = true;
    public void OnButtonClick()
    {
        AudioManager.Instance.AudioButtonClick();
        if (canClick)
        {
            PlayerPrefs.SetInt("SelectedLevel", numLevel);
            PlayerPrefs.Save();
            sceneFader.FadeTo("GamePlay");
        }

    }

    private void Awake()
    {
        Assert.IsNotNull(currentButton);
        Assert.IsNotNull(lockedButton);
        Assert.IsNotNull(buttonImg);
        Assert.IsNotNull(txtNumberLevel);
        Instance = this;
        txtNumberLevel.text = (numLevel+1).ToString();
        int nb =PlayerPrefs.GetInt("CompletedLevel");
        nextLevel = nb;
    }

    private void Start()
    {
        button = GetComponent<Button>();
        if (numLevel == nextLevel) buttonImg.sprite = currentButton;

        else if (numLevel < nextLevel) buttonImg.sprite = currentButton;

        else
        {
            buttonImg.sprite = lockedButton;
            txtNumberLevel.gameObject.SetActive(false);
            canClick = false;
        }
    }
}
