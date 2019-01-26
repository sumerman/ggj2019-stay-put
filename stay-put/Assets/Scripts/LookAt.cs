using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;
    private Vector3 relativePosition;
    // Start is called before the first frame update
    void Start()
    {
        relativePosition = gameObject.transform.position - target.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + relativePosition;
        transform.LookAt(target);
    }
}
