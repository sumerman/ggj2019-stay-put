using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(ParticleSystem))]
public class DirectionIndicator : MonoBehaviour
{
    public Transform target;
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

            emitter.transform.position = Vector3.Lerp(Camera.main.transform.position, target.position, 0.2f);
            var dist = Vector3.Distance(Camera.main.transform.position, target.position);
            var shapeMod = emitter.shape;
            shapeMod.radius =  dist * 0.1f;
            emitter.emissionRate = dist * 0.9f;
            emitter.startSize = dist / 6f;
        } 
        else if (emitter.isEmitting)
        {
            emitter.Stop();
        }
    }
}
