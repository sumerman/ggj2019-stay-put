using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;
using Kino;

[RequireComponent(typeof(PostProcessVolume))]
[RequireComponent(typeof(DigitalGlitch))]
[RequireComponent(typeof(AnalogGlitch))]
[RequireComponent(typeof(Camera))]

public class GlitchController : MonoBehaviour
{
    public delegate void PropertyMutator(float value);
    public Transform player;
    public Transform origin;

    public float safeRadius;
    public float maxRadius;

    private List<PropertyMutator> mutators;

    // Start is called before the first frame update
    void Start()
    {
        mutators = new List<PropertyMutator>();

        foreach(PostProcessVolume v in this.GetComponents<PostProcessVolume>()) {
            mutators.Add((float x) => {
                v.weight = x;
            });
        }
        
        DigitalGlitch digitalGlitch = this.GetComponent<DigitalGlitch>();
        mutators.Add((float x) => { digitalGlitch.intensity = x * 0.8f; });

        AnalogGlitch analogGlitch = this.GetComponent<AnalogGlitch>();
        mutators.Add((float x) => { analogGlitch.scanLineJitter = x * 0.3f; });
        mutators.Add((float x) => { analogGlitch.verticalJump = x * 0.4f; });
    }

    // Update is called once per frame
    void Update()
    {
        float d = 0.0f;

        if(player.gameObject.activeSelf) {
            d = Mathf.Clamp((Vector3.Distance(player.position, origin.position) - safeRadius) / maxRadius, 0.0f, 1.0f);
        }

        foreach(PropertyMutator m in mutators) {
           m.Invoke(d); 
        }
    }
}
