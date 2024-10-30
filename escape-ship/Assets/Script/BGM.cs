using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource has been assigned
        if (audioSource != null)
        {
            audioSource.loop = true;  // Enable looping
            audioSource.Play();       // Start playing the audio
        }
        else
        {
            Debug.LogWarning("AudioSource component missing on " + gameObject.name);
        }
    }
}
