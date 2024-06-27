using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{



    public void BuyBom()
    {
        RfHolder.Instance.BomAmount();
    }
}
