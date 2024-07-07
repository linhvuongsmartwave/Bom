using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;
using TMPro;

public class RfHolder : MonoBehaviour
{
    private TextMeshProUGUI txtHeart;
    private TextMeshProUGUI txtExplosion;
    private TextMeshProUGUI txtSpeed;
    private TextMeshProUGUI txtBom;
    public static RfHolder Instance;
    private BomControl bomControl;
    private Player player;

    private void Awake()
    {
        Instance = this;
        txtHeart = GameObject.Find("txtHeart").GetComponent<TextMeshProUGUI>();
        txtExplosion = GameObject.Find("txtExplosion").GetComponent<TextMeshProUGUI>();
        txtSpeed = GameObject.Find("txtSpeed").GetComponent<TextMeshProUGUI>();
        txtBom = GameObject.Find("txtBom").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (player != null )
        {
            print("tim thay player rfholder");
            txtHeart.text = (player.currentHealth).ToString();
            txtSpeed.text = player.speedMove.ToString();
            txtExplosion.text = 1.ToString();
            txtBom.text = 1.ToString();
        }
        bomControl = FindObjectOfType<BomControl>();
    }

    public void PutBom()
    {
        bomControl.PutBom();
    }
    public void BomAmount()
    {
        bomControl.bomRemaining += 1;
    }
    public void Radius()
    {
        bomControl.radius += 1;
    }
    public void Speed()
    {
        player.speedMove += 2;
    }

    public void BtnClick()
    {
        AudioManager.Instance.AudioButtonClick();
    }
    public void OpenPopup()
    {
        AudioManager.Instance.AudioOpen();
    }
    public void Vibrate()
    {
        HapticFeedback.LightFeedback();
    }

    public void ShowIconPushBom()
    {
        bomControl.ShowIconPushBom();
    }
    public void HideIconPushBom()
    {
        bomControl.HideIconPushBom();
    }


}