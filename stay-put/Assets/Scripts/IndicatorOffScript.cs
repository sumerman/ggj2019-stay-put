using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorOffScript : MonoBehaviour
{
    public DirectionIndicator indicator;
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        indicator.gameObject.SetActive(false);
    }
}
