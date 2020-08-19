using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfishSound : MonoBehaviour
{
    private AudioSource source;
    public AudioClip clip;
    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time >= nextSoundEffectTime)
        {
            source.clip = clip;
            source.Play();
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
    }
}
