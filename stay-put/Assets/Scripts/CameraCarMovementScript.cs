using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCarMovementScript : MonoBehaviour
{
    public float lagFactor;
    public GameObject target;

    private float distanceToTarget;

    void Awake()
    {
        distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
