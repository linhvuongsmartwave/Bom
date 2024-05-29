using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RfHolder : MonoBehaviour
{
    private BomControl bomControl;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        bomControl = FindObjectOfType<BomControl>();
        player = FindObjectOfType<Player>();
        Debug.Log("bombAmount: " + bomControl.bomRemaining);

    }

    public void PutBom()
    {
        bomControl.PutBom();
    }
    public void BomAmount()
    {
        bomControl.bomRemaining += 1;
        Debug.Log("bombAmount: "+ bomControl.bomRemaining);
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
