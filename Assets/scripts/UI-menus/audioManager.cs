using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    //music
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]
    public AudioClip background;
    public AudioClip menuSelect;

    public static audioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        musicSource.clip = background;
       // musicSource.Play();
       // if ()
       // { 
        
        //}
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
 }
