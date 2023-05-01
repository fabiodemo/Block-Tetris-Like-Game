using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public void PlaySoundGameOver()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

}
