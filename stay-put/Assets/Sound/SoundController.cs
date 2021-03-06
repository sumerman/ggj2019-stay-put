﻿using System;
using System.Collections.Generic;
using UnityEngine;
using GoldenAudio;

public class SoundController : MonoBehaviour
{
    private int staticWaveVoice = -1;
    private int wobbleVoice = -1;
    private double wobbleFreq;
    private double wobbleLFOFreq;
    private double wobbleLFOAmount;
    private double wobbleDuration;
    private double wobbleStart;
    private bool wobblingNow;

    private Queue<int> sinesQueue;

    public AudioSource zztPlayer;
    public AudioClip zztSound;
    public SineSynthPoly synth;

    [Range(0f, 1f)]
    public float zztItensity = 0.0f;
    [Range(0f, 1f)]
    public float wobbleIntensity = 0.0f;
    [Range(0f, 1f)]
    public float sineIntensity = 0.0f;
    [Range(0f, 1f)]
    public float staticWaveIntensity = 0.0f;
    public int staticWaveBaseFreq = 1400;

    private void Start() {
        sinesQueue = new Queue<int>();
        zztPlayer.clip = zztSound;
        zztPlayer.volume = 0.2f;
        staticWaveVoice = synth.NewSine(staticWaveBaseFreq, 0);
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
        } else {
            if (UnityEngine.Random.value < wobbleIntensity * 0.05f) {
                RandomiseWobble(wobbleIntensity);
                wobbleDuration = 0.5f;
                wobbleStart = Time.time;
                wobbleVoice = synth.NewSine(wobbleFreq + wobbleLFOAmount * Math.Sin(wobbleLFOFreq * Time.time), 0.2, 1);
                wobblingNow = true;
            }
        }
        // LFO Modulate
        synth.SetFreq(wobbleVoice, wobbleFreq + wobbleLFOAmount * Math.Sin(wobbleLFOFreq * Time.time));
    }

    private void ManageStaticWave() {
        synth.SetAmp(staticWaveVoice, staticWaveIntensity);
        synth.SetFreq(staticWaveVoice, staticWaveBaseFreq + staticWaveIntensity * 500);
    }

    private void ManageSines() {
        if (sinesQueue.Count < sineIntensity * 100) {
            int voice = synth.NewSine(UnityEngine.Random.value * 800 + 200, 0.03, 2);
            sinesQueue.Enqueue(voice);
        }

        if (sinesQueue.Count > (sineIntensity + 0.01f) * 100) {
            int voice = sinesQueue.Dequeue();
            synth.ReleaseVoice(voice, 0.05f);
        }

        if (sineIntensity == 0f) {
            while(sinesQueue.Count > 0) {
                synth.ReleaseVoice(sinesQueue.Dequeue(), 0.01f);
            }
        }
    }

    public void ReleaseAll() {
        synth.ReleaseAll(Release: 2.5);
    }

    private void ManageZztGlitch() {
        if (UnityEngine.Random.value < zztItensity) {
            zztPlayer.Play();
        }
    }


    // Update is called once per frame
    void Update() {
        ManageWobbles();
        ManageSines();
        ManageZztGlitch();
        ManageStaticWave();
    }
}
