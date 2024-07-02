using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        FindBomControl();
        player = FindObjectOfType<Player>();
    }

    public void FindBomControl()
    {
        bomControl = FindObjectOfType<BomControl>();
        if (bomControl != null)
        {
            print("tìm thấy thằng chứa script bom để đặt bom");
        }
        else
        {
            print("không tìm thấy thằng chữa script bom để đặt bom");

        }

    }


    public void PutBom()
    {
        bomControl.PutBom();
    }
    public void BomAmount()
    {
        Debug.Log("BomAmount");
        bomControl.bomRemaining += 1;
        Debug.Log("bomRemaining : " + bomControl.bomRemaining);
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


}