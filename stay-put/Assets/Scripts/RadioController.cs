using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadioController : MonoBehaviour
{
    public AudioClip[] songs;
    private int currentSong = 0;
    public AudioSource source;

    void Awake()
    {
        //source = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        source.clip = songs[currentSong];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        {
            currentSong = (currentSong + 1) % songs.Length;
            source.clip = songs[currentSong];
        }
    }
}
