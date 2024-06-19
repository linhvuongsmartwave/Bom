﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public GameObject[] buttons;
    public int characterSelect;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);


        characterSelect = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[characterSelect].SetActive(true);

    }

    public void Next()
    {
        skins[characterSelect].SetActive(false);
        characterSelect++;
        if (characterSelect == skins.Length) characterSelect = 0;

        skins[characterSelect].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", 0);


        if (PlayerPrefs.GetInt("Character_" + (characterSelect) + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSelect);
        }
    }
    public void Back()
    {
        skins[characterSelect].SetActive(false);
        characterSelect--;
        if (characterSelect == -1) characterSelect = skins.Length - 1;

        skins[characterSelect].SetActive(true);
        PlayerPrefs.SetInt("SelectedCharacter", 0);

        if (PlayerPrefs.GetInt("Character_" + (characterSelect) + "_Bought", 0) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", characterSelect);
        }
    }

    public void BuyCharacter(int characterIndex)
    {

        PlayerPrefs.SetInt("Character_" + (characterIndex) + "_Bought", 1);
        buttons[characterIndex].SetActive(false);
    }
    private void Update()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(i == characterSelect - 1);
            if (PlayerPrefs.GetInt("Character_" + (i + 1) + "_Bought", 0) == 1)
            {
                buttons[i].SetActive(false);
            }
        }
    }
}