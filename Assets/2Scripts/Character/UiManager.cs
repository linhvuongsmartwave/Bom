using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public GameObject panelWin;

    
    public void PanelWin()
    {

        panelWin.SetActive(true);
    }
}
