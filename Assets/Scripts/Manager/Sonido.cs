using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sonido : MonoBehaviour
{
    private AudioSource music;
    public AudioClip ClickAudio;
    public AudioClip SwitchAudio;

    public void Start()
    {
        music = GetComponent<AudioSource>();
    }

    public void ClickAudioOn()
    {
        music.PlayOneShot(ClickAudio);
    }

    public void SwitchAudioOn()
    {
        music.PlayOneShot(SwitchAudio);
    }
}
