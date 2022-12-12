using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodleMusic : MonoBehaviour
{
     public AudioSource doodleMusic;
     private void Awake()
     {
         DontDestroyOnLoad(transform.gameObject);
         doodleMusic = GetComponent<AudioSource>();
     }
 
     public void PlayMusic()
     {
         if (doodleMusic.isPlaying) return;
         doodleMusic.Play();
     }
 
     public void StopMusic()
     {
         doodleMusic.Stop();
     }
 }