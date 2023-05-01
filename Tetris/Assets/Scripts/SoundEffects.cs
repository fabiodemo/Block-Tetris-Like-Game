using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource levelMusic;
    public AudioSource deathSound;
    public AudioSource moveSong;
    public AudioSource RotateSong;

    public bool levelSong = true;
    public bool DeathSong = false;
    // Start is called before the first frame update
  
    public void LevelMusic()
    {
        levelSong = true;
        DeathSong = false;
        levelMusic.Play();
    }

    public void DeathSound()
    {
        if (levelMusic.isPlaying)
        {
            levelSong = false;
            levelMusic.Stop();
        }
        if(!deathSound.isPlaying && DeathSong == false)
        {
            deathSound.Play();
            DeathSong = true;
        }
    }

    public void MoveSound()
    {
        moveSong.Play();
    }

    public void RotateSound()
    {
        RotateSong.Play();
    }
}
