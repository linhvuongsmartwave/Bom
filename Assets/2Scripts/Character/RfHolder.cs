using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyCoded.HapticFeedback;

public class RfHolder : MonoBehaviour
{
    public static RfHolder Instance;
    private BomControl bomControl;
    private Player player;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
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


}