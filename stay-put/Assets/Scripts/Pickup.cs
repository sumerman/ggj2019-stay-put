using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupStats stats;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e") && GetComponentInChildren<PickupTrigger>().playerIsInArea)
        {
            Debug.Log("picked up");
            this.gameObject.SetActive(false);

            GameObject ui = GameObject.FindGameObjectWithTag("UI");
            if (ui)
            {
                ScreenNotifications notifications =
                    GameObject.FindGameObjectWithTag("UI").GetComponent<ScreenNotifications>();
                if (notifications)
                {
                    notifications.SetText("");
                }
            }
        }
    }
}
