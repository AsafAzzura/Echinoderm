using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFishSounds : MonoBehaviour
{
    private AudioSource source;
    public AudioClip[] clips;
    public float timeBetweenSoundEffects;
    private float nextSoundEffectTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0.3f;
    }

    private void Update()
    {
        if (Time.time >= nextSoundEffectTime)
        {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
            nextSoundEffectTime = Time.time + timeBetweenSoundEffects;
        }
    }
}
