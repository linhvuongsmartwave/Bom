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
}