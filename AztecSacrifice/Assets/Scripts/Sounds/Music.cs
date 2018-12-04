using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public AudioClip[] Musics;

    AudioSource audio;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        audio = GetComponent<AudioSource>();

        int index = Random.Range(0, Musics.Length);

        audio.clip = Musics[index];

        audio.Play();
    }

}
