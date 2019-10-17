using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance = null;

    public AudioSource efxSource, musicSource, ambienteSource, playerSound;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    private IEnumerator coroutineFadeOut, coroutineFadeIn;

    private void Update()
    {

    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);

    }

    public void FadeIn(AudioSource audioSource, float targetVolume, float fadeSpeed)
    {
        if (coroutineFadeOut != null)
        {
            StopCoroutine(coroutineFadeOut);
        }
        coroutineFadeIn = FadeInCorrotina(audioSource, targetVolume, fadeSpeed);
        StartCoroutine(coroutineFadeIn);
    }

    public void FadeOut(AudioSource audioSource, float targetVolume, float fadeSpeed)
    {
        if (coroutineFadeIn != null)
        {
            StopCoroutine(coroutineFadeIn);
        }
        coroutineFadeOut = FadeOutCorrotina(audioSource, targetVolume, fadeSpeed);
        StartCoroutine(coroutineFadeOut);

    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    public static IEnumerator FadeInCorrotina(AudioSource audioSource, float targetVolume, float fadeSpeed)
    {
        while (audioSource.volume < targetVolume)
        {
            float tempVolume = audioSource.volume + Time.deltaTime * fadeSpeed;
            audioSource.volume = Mathf.Min(tempVolume, targetVolume);
            yield return null;
        }
    }

    public static IEnumerator FadeOutCorrotina(AudioSource audioSource, float targetVolume, float fadeSpeed)
    {
        while (audioSource.volume > targetVolume)
        {
            float tempVolume = audioSource.volume - Time.deltaTime * fadeSpeed;
            audioSource.volume = tempVolume;
            yield return null;
        }
    }
}