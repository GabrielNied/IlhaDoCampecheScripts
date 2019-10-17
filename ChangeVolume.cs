using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeVolume : MonoBehaviour
{

    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = 1;
    }

    void Update()
    {
        VolumeController();
    }

    public void VolumeController()
    {

        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (volumeSlider.value < 0.9f)
            {
                SoundManager.instance.musicSource.volume = volumeSlider.value;
            }
        }
        else
        {
            SoundManager.instance.musicSource.volume = volumeSlider.value;
            SoundManager.instance.efxSource.volume = volumeSlider.value;
            SoundManager.instance.playerSound.volume = volumeSlider.value;
            SoundManager.instance.ambienteSource.volume = volumeSlider.value;
        }
    }
}
