using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadioController : MonoBehaviour
{
    public AudioClip[] songs;
    private int currentSong = 0;
    public AudioSource source;

    private bool isInIntro = false;

    void Awake()
    {
        //source = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject fadeAnimatior = GameObject.FindGameObjectWithTag("FadeAnimations");
        if (fadeAnimatior)
        {
            isInIntro = true;
            FadeAnimationPlayer changer = fadeAnimatior.GetComponent<FadeAnimationPlayer>();
            changer.IntroComplete.AddListener(IntroDone);
        }
        else
        {
            source.clip = songs[currentSong];
            source.Play();
        }
    }

    public void IntroDone()
    {
        isInIntro = false;
        source.clip = songs[currentSong];
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInIntro) return;

        if(!source.isPlaying)
        {
            currentSong = (currentSong + 1) % songs.Length;
            source.clip = songs[currentSong];
        }
    }
}
