using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSoure;
    public AudioClip bomExp;
    public AudioClip coolDown;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSoure = GetComponent<AudioSource>();
    }

    public void BomExp()
    {
        audioSoure.PlayOneShot(bomExp);
    }    

    public void CoolDown()
    {
        audioSoure.PlayOneShot(coolDown);
    }
}
