using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{

    private AudioSource source;
    public AudioClip[] clips;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        int randomNumber = Random.Range(0, clips.Length);
        source.clip = clips[randomNumber];
        if (randomNumber == 0)
        {
            source.volume = 0.2f; // too much load;
        }
        else
        {
            source.volume = 0.5f;
        }
        source.Play();
    }

}
