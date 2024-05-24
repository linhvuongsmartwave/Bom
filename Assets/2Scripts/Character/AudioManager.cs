using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSoure;
    public AudioClip bomExp;
    public AudioClip coolDown;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSoure = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
