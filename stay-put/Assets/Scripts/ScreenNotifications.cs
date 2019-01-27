using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNotifications : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator) animator.enabled = false;
    }

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

            if (animator && text == " ")
            {
                animator.enabled = true;
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
        //RawImage image = 
        if (animator) animator.enabled = false;
    }
}
