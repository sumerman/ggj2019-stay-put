using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using Kino;

[RequireComponent(typeof(PostProcessVolume))]
[RequireComponent(typeof(DigitalGlitch))]
[RequireComponent(typeof(AnalogGlitch))]
[RequireComponent(typeof(SoundController))]
[RequireComponent(typeof(Camera))]

public class GlitchController : MonoBehaviour
{
    public delegate void PropertyMutator(float value);

    struct Segment {
        public float from, to;
        public PropertyMutator mutator;

        public Segment(float f, float t, PropertyMutator m) 
        {
            from = f;
            to = t;
            mutator = m;
        }
    }

    public Transform player;
    public Transform origin;

    public float safeRadius = 1.0f;
    public float maxRadius = 10.0f;
    public float deltaSpawn = 0.1f;
    public float minLength = 0.6f;

    private List<PropertyMutator> mutators;
    private List<Segment> segments;

    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        mutators = new List<PropertyMutator>();
        segments = new List<Segment>();

        foreach(PostProcessVolume v in this.GetComponents<PostProcessVolume>()) 
        {
            mutators.Add((float x) => { v.weight = x; });
        }

        SoundController glitchSounds = this.GetComponent<SoundController>();
        
        DigitalGlitch digitalGlitch = this.GetComponent<DigitalGlitch>();
        mutators.Add((float x) => {
             digitalGlitch.intensity = x * 0.8f;
             glitchSounds.zztItensity = x * 0.9f;
        });

        AnalogGlitch analogGlitch = this.GetComponent<AnalogGlitch>();
        mutators.Add((float x) => { 
            analogGlitch.scanLineJitter = x * 0.7f;
            glitchSounds.sineIntensity = x * 0.5f;
        });
        mutators.Add((float x) => { 
            analogGlitch.verticalJump = x * 0.4f;
            glitchSounds.wobbleIntensity = Mathf.Clamp01(x - 0.1f);
        });
    }

    // Update is called once per frame
    void Update()
    {
        float totalIntensity = 0.0f, lastSegmentFrom = -2.0f;

        if (player.gameObject.activeSelf) 
        {
            var dist = Vector3.Distance(player.position, origin.position);
            totalIntensity = Mathf.Clamp01((dist - safeRadius) / maxRadius);
        }

        while (true)
        {
            int lastSegIdx = segments.FindLastIndex(s => totalIntensity <= s.from);
            if (lastSegIdx > -1)
            {
                var mutator = segments[lastSegIdx].mutator;
                mutator.Invoke(0.0f);
                mutators.Add(mutator);
                segments.RemoveAt(lastSegIdx);
            }
            else
            {
                break;
            }
        }

        foreach (Segment s in segments)
        {
            lastSegmentFrom = s.from;
            float value = Mathf.InverseLerp(s.from, s.to, totalIntensity);
            s.mutator.Invoke(value);
        }

        if (lastSegmentFrom + deltaSpawn <= totalIntensity && mutators.Count > 0) 
        {
            if (Random.value < totalIntensity) {
                int mutIdx = Random.Range(0, mutators.Count);
                float to = Random.Range(totalIntensity + minLength, 1.0f);
                segments.Add(new Segment(totalIntensity, to, mutators[mutIdx]));
                mutators.RemoveAt(mutIdx);
            }
        }
    }
}
