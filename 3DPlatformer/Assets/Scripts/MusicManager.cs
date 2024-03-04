using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    public AudioSource[] bgMusic;
    public int musicIndextToPlay;

    private int currentTrack;

    public AudioSource[] fx;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTrack = musicIndextToPlay;
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            currentTrack++;
            if(currentTrack >= bgMusic.Length)
            {
                currentTrack = 0;
            }
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        for (int i = 0; i < bgMusic.Length; i++)
        {
            bgMusic[i].Stop();
        }
        bgMusic[currentTrack].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        fx[sfxToPlay].Play();
    }
}
