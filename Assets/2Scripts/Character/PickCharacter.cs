using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickCharacter : MonoBehaviour
{
    public GameObject male;
    public GameObject feMale;
    public UiPanelDotween ui;
    void Start()
    {
        male.SetActive(false);
        feMale.SetActive(false);
        Invoke("Wait",1f);
    }

    void Update()
    {

    }

    public void Male()
    {
        male.SetActive(true);
        ui.PanelFadeOut();
    }
    public void FeMale()
    {
        feMale.SetActive(true);
        ui.PanelFadeOut();
    }

    void Wait()
    {
        ui.PanelFadeIn();

    }
}
