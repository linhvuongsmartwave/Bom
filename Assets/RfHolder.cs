using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RfHolder : MonoBehaviour
{
    private BomControl bomControl;
    // Start is called before the first frame update
    void Start()
    {
        bomControl = FindObjectOfType<BomControl>();
    }

    public void PutBom()
    {
        bomControl.PutBom();
    }

}
