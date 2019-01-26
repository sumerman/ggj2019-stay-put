using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNotifications : MonoBehaviour
{
    public void SetText(string text)
    {
        Text notificationText = GetComponentInChildren<Text>();
        if (notificationText)
        {
            notificationText.text = text;
        }
    }
}
