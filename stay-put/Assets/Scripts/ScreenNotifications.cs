using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNotifications : MonoBehaviour
{
    public void SetText(string text)
    {
        Button notification = GetComponentInChildren<Button>();
        if (notification)
        {
            Text buttonText = notification.GetComponentInChildren<Text>();
            if(buttonText)
            {
                notification.image.enabled = true;
                buttonText.text = text;
            }
        }
    }
    public void DisableNotification()
    {
        Button notification = GetComponentInChildren<Button>();
        if (notification)
        {
            Text buttonText = notification.GetComponentInChildren<Text>();
            if (buttonText)
            {
                notification.image.enabled = false;
                buttonText.text = "";
            }
        }
    }
}
