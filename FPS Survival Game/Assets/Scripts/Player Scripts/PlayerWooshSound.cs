using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWooshSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip woosh_Sounds;

    void PlayWooshSound()
    {
        audioSource.clip = woosh_Sounds;
        audioSource.Play();
    }
}
