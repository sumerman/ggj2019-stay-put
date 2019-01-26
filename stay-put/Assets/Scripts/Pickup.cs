using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PlayerCharacter player;
    public Application.PickupStats stats;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && GetComponentInChildren<PickupTrigger>().playerIsInArea)
        {
            player.onPickedUpObject();
            this.gameObject.SetActive(false);
            ScreenNotifications notifications =
                GameObject.FindGameObjectWithTag("UI").GetComponent<ScreenNotifications>();
            if(notifications)
            {
                notifications.SetText("");
            }
        }
    }
}
