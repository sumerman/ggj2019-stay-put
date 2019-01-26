using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorSoundController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip startSound;
    public AudioClip driveSound;
    public AudioClip stopSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSound()
    {
        source.loop = false;
        source.clip = stopSound;
        source.Play();
    }

    public void StartSound()
    {
        source.loop = false;
        source.clip = startSound;
        source.Play();
    }

    public void DriveSound()
    {

    }
}
