using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool playerIsInArea;
    private ScreenNotifications notifications;
    // Start is called before the first frame update
    void Start()
    {
        playerIsInArea = false;
        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui) notifications = ui.GetComponent<ScreenNotifications>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter pc = other.gameObject.GetComponent<PlayerCharacter>();
        if (pc == null) return;

        Debug.Log("entered");
        playerIsInArea = true;
        if (notifications) notifications.SetText("E");
    }

    private void OnTriggerExit(Collider other)
    {
        playerIsInArea = false;
        if (notifications) notifications.DisableNotification();
    }
}