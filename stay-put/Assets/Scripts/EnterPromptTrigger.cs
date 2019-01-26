using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPromptTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter pc = other.gameObject.GetComponent<PlayerCharacter>();
        if (pc == null) return;
        if (!pc.IsOnSpawnFrame())
        {
            pc.EnableCarEnter();
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerCharacter pc = other.gameObject.GetComponent<PlayerCharacter>();
        if (pc == null) return;
        pc.DisableCarEnter();
    }
}
