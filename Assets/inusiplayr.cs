﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inusiplayr : MonoBehaviour
{
    public bool randomPlay = false; // checkbox for random play
    public AudioClip[] clips;
    private AudioSource audioSource;// = this.gameObject.GetComponent<AudioSource>();
    int clipOrder = 0; // for ordered playlist

    void Start () {
        audioSource = GetComponent<AudioSource> ();
        audioSource.loop = false;
        //clips..AudioClip a = (AudioClip)Resources.Load("Russian_Dance");
        //AudioClip.Create("Russian_Dance");

    }

    void Update () {
        if (!audioSource.isPlaying) {
            // if random play is selected
            if (randomPlay == true) {
                audioSource.clip = GetRandomClip ();
                audioSource.Play ();
                // if random play is not selected
            } else {
                audioSource.clip = GetNextClip ();
                audioSource.Play ();
            }
        }
    }

    // function to get a random clip
    private AudioClip GetRandomClip () {
        return clips[Random.Range (0, clips.Length)];
    }

    // function to get the next clip in order, then repeat from the beginning of the list.
    private AudioClip GetNextClip () {
        if (clipOrder >= clips.Length - 1) {
            clipOrder = 0;
        } else {
            clipOrder += 1;
        }
        return clips[clipOrder];
    }

    void Awake () {
        //DontDestroyOnLoad (transform.gameObject);
    }
}


