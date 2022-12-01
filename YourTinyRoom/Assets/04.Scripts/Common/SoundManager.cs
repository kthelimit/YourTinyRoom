using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public float sfxVolume = 1.0f; // 볼륨
    public bool isSfxMute = false; //뮤트여부
    public static SoundManager soundManager;//싱글턴
    public Slider audioslider;

    void Awake()
    {
        if (soundManager == null)
            soundManager = this;
        else if (soundManager != this)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySfx(Vector3 pos, AudioClip sfx)
    {
        if (isSfxMute) return;
        GameObject soundobj = new GameObject("Sfx");
        soundobj.transform.position = pos;
        AudioSource audioSource = soundobj.AddComponent<AudioSource>();
        audioSource.clip = sfx;
        audioSource.minDistance = 10f;
        audioSource.maxDistance = 40f;
        audioSource.volume = sfxVolume;
        audioSource.Play();
        Destroy(soundobj, sfx.length);
    }

    public void SetVolume(float sliderValue)
    {
        sfxVolume = sliderValue;
    }

    public void PlaySfx(Vector3 pos, AudioClip bgm, bool loop)
    {
        if (isSfxMute) return;
        GameObject soundobj = new GameObject("Sfx");
        soundobj.transform.position = pos;
        AudioSource audioSource = soundobj.AddComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.minDistance = 10f;
        audioSource.maxDistance = 40f;
        audioSource.volume = sfxVolume;
        audioSource.loop = loop;
        audioSource.Play();
    }

}
