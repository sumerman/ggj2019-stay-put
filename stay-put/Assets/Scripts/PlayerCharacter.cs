using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public Car car;

    private bool waitAFrame = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello");
        if (Input.GetKeyDown("e"))
        {
            car.onPlayerEntered();
            this.gameObject.SetActive(false);
        }
    }

    public void onPickedUpObject()
    {
        Debug.Log("picked up");
    }
}
