using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologoManager : MonoBehaviour
{
    private TextManager tManager;
    private FadeManager fManager;

    public bool chamaHistoria = true;

    public AudioClip trilha, porta;

    private float volumeMusica;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("TextManager") != null)
        {
            tManager = GameObject.FindGameObjectWithTag("TextManager").GetComponent<TextManager>();
        }
        if (GameObject.FindGameObjectWithTag("FadeManager") != null)
        {
            fManager = GameObject.FindGameObjectWithTag("FadeManager").GetComponent<FadeManager>();
        }
        tManager.parteHistoria = 0;
        volumeMusica = SoundManager.instance.musicSource.volume;
        SoundManager.instance.FadeOut(SoundManager.instance.musicSource, 0.0f, 0.5f);               
    }

    void Update()
    {
        
        if (SoundManager.instance.musicSource.volume <= 0.0f)
        {
            SoundManager.instance.musicSource.clip = trilha;
            SoundManager.instance.musicSource.Play();
            SoundManager.instance.FadeIn(SoundManager.instance.musicSource, volumeMusica, 0.25f);
        }
        
        if (chamaHistoria)
        {
            if (!Cursor.visible)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            StartCoroutine(tManager.Type());
            chamaHistoria = false;
        }
        
        if (tManager.historiaCompleta && tManager.parteHistoria <= 5)
        {
            tManager.parteHistoria += 1;
            chamaHistoria = true;
            tManager.historiaCompleta = false;
        }

        if (tManager.historiaCompleta && tManager.parteHistoria == 6)
        {
            SoundManager.instance.efxSource.clip = porta;
            SoundManager.instance.efxSource.Play();
            fManager.ChamaCena(tManager.sceneToLoad);
        }
        
    }
}
