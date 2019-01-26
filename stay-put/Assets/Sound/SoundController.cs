using System;
using UnityEngine;
using GoldenAudio;

public class SoundController : MonoBehaviour
{
    private int wobbleVoice = -1;
    private double wobbleFreq;
    private double wobbleLFOFreq;
    private double wobbleLFOAmount;
    private double wobbleDuration;
    private double wobbleStart;
    private bool wobblingNow;

    public AudioSource zztPlayer;
    public AudioClip zztSound;
    private float zztDensity;
    private float zztEnd;
    private bool zztImmediately;

    public SineSynthPoly synth;
    public bool TestMode;


    private void Start() {
        zztPlayer.clip = zztSound;
        zztPlayer.volume = 0.2f;
    }


    // Wobble effect
    public void Wobble(double Duration, float Intensity = 1) {
        // Only one wobble at a time
        if (wobblingNow) {
            return;
        }
        RandomiseWobble(Intensity);
        wobbleDuration = Duration;
        wobbleStart = Time.time;
        wobbleVoice = synth.NewSine(wobbleFreq + wobbleLFOAmount * Math.Sin(wobbleLFOFreq * Time.time), 0.2, 1);
        wobblingNow = true;
    }

    private void RandomiseWobble(float Intensity) {
        wobbleFreq = UnityEngine.Random.value * 800 + 200;
        wobbleLFOFreq = UnityEngine.Random.value * Intensity * 24 + 1;
        wobbleLFOAmount = UnityEngine.Random.value * wobbleFreq * 0.75 + 100;
    }

    private void ManageWobbles() {
        if (wobblingNow) {
            // Check for end
            if (Time.time > wobbleStart + wobbleDuration) {
                synth.ReleaseVoice(wobbleVoice, 1);
                wobblingNow = false;
            }
        }
        // LFO Modulate
        synth.SetFreq(wobbleVoice, wobbleFreq + wobbleLFOAmount * Math.Sin(wobbleLFOFreq * Time.time));
    }


    // Get spookier with Sine wave
    public void SinePad(int number) {
        for (int i = 0; i < number; i++) {
            synth.NewSine(UnityEngine.Random.value * 800 + 200, 0.03, 2);
        }
    }

    public void ReleaseAll() {
        synth.ReleaseAll(Release: 2.5);
    }


    // Zzt Glitch
    public void ZztGlitch(float Density, float Duration) {
        zztDensity = Density;
        zztEnd = Time.time + Duration;
        zztImmediately = true;
    }

    private void ManageZztGlitch() {
        if (zztImmediately || zztEnd > Time.time && UnityEngine.Random.value < zztDensity) {
            zztPlayer.Play();
            zztImmediately = false;
        }
    }


    // Update is called once per frame
    void Update() {
        if (TestMode) {
            if (Input.GetKeyDown(KeyCode.A)) {
                SinePad(3);
            }
            if (Input.GetKeyDown(KeyCode.Y)) {
                ReleaseAll();
            }
            if (Input.GetKeyDown(KeyCode.W)) {
                ZztGlitch(0.75f, 1);
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                ZztGlitch(0.07f, 3);
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                Wobble(1, Intensity: 1);
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                Wobble(1, Intensity: 10);
            }

        }

        ManageWobbles();

        ManageZztGlitch();
    }
}
