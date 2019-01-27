using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(ParticleSystem))]
public class DirectionIndicator : MonoBehaviour
{
    public Transform target;
    public Camera cam;
    public float radA = 0.2f, rateA = 2.5f, distDiv = 6f, lerp = 0.17f;
    private ParticleSystem emitter;
    // Start is called before the first frame update
    void Start()
    {
       emitter =  this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) 
        {
            if (!emitter.isEmitting) {
                emitter.Play();
            }

            emitter.transform.position = Vector3.Lerp(cam.transform.position, target.position, lerp);
            var dist = Vector3.Distance(cam.transform.position, target.position);
            var shapeMod = emitter.shape;
            shapeMod.radius =  dist * radA;
            emitter.emissionRate = dist * rateA;
            emitter.startSize = dist / distDiv;
        } 
        else if (emitter.isEmitting)
        {
            emitter.Stop();
        }
    }
}
