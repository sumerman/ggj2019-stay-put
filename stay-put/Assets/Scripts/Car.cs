using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GameObject.FindGameObjectWithTag("StopPoint");
        if (target != null)
        {
            Vector3 stopPosition = target.GetComponent<Rigidbody>().position;
            GetComponent<Rigidbody>().position = Vector3.MoveTowards(GetComponent<Rigidbody>().position, stopPosition, speed);
        }
    }
}
