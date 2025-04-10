using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource soundFXObject;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in the GameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioClip
        audioSource.clip = audioClip;

        //assign the volume
        audioSource.volume = volume;

        //play the sound
        audioSource.Play();

        //get the length of sound clip
        float clipLength = audioSource.clip.length;

        //destroy the object after it is done playing
        Destroy(audioSource.gameObject, clipLength);
    }


}
