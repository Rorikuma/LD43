using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

    AudioSource audio;

    public void PlayClip(AudioClip clip)
    {
        audio.clip = clip;
        audio.Play();
    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

}
