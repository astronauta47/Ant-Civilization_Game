using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    AudioSource audioSource;
    public static bool attack;

    float delay = 120;

    public static float timer = 120;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!attack)
        {
            timer += Time.deltaTime;
            delay = 120;

            if (timer >= delay)
            {
                audioSource.PlayOneShot(sounds[0]);
                timer = 0;
            }
        }
        else
        {
            delay = 80;

            if (timer >= delay)
            {
                audioSource.PlayOneShot(sounds[1]);
                timer = 0;
            }
        }

    }

    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(sounds[index]);
    }

}
