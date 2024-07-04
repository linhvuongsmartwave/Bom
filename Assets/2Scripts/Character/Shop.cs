using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI txtGold;

    public int gold;

    private void Start()
    {
        Load();
    }

    public void BuyGold(int gold)
    {
        this.gold += gold;
        Save();
        UpdateGold();


    }
    private void Update()
    {
        print("gold : "+gold);
    }


    public void Load()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        UpdateGold();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("gold", gold);
        PlayerPrefs.Save();

    }

    public void UpdateGold()
    {
        txtGold.text = gold.ToString();

    }


}
