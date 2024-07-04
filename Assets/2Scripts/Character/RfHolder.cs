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
        //FindBomControl();
        player = FindObjectOfType<Player>();
        bomControl = FindObjectOfType<BomControl>();
    }

    //public void FindBomControl()
    //{
    //    bomControl = FindObjectOfType<BomControl>();
    //    if (bomControl != null)
    //    {
    //        print("tìm thấy thằng chứa script bom để đặt bom");
    //    }
    //    else
    //    {
    //        print("không tìm thấy thằng chữa script bom để đặt bom");

    //    }

    //}


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
    //public void IconFalse()
    //{
    //    FindBomControl();
    //    bomControl.IconFalse();
    //} 
    //public void IconTrue()
    //{
    //    FindBomControl();
    //    bomControl.IconTrue();
    //}
    public void Vibrate()
    {
        HapticFeedback.LightFeedback();
    }


}