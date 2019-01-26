using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool playerIsInArea;
    public PlayerCharacter player;
    // Start is called before the first frame update
    void Start()
    {
        playerIsInArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && playerIsInArea)
        {
            player.onPickedUpObject();
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        playerIsInArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsInArea = false;
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
        // Show "press e" to pick up

    }
}
