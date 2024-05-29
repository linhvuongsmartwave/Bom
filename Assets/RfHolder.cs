using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RfHolder : MonoBehaviour
{
    private BomControl bomControl;
    private Player player;
    void Start()
    {
        bomControl = FindObjectOfType<BomControl>();
        player = FindObjectOfType<Player>();
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
}
