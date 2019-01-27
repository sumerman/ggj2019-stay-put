using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerController : MonoBehaviour
{
    public GameObject leftLight;
    public GameObject rightLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator StartBreaking()
    {
        leftLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        leftLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        leftLight.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        leftLight.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        leftLight.SetActive(false);
        yield return new WaitForSeconds(1.2f);
        rightLight.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        rightLight.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        rightLight.SetActive(false);

    }

    public void TurnLeftOn()
    {
        leftLight.SetActive(true);
    }

    public void TurnOn()
    {
        leftLight.SetActive(true);
        rightLight.SetActive(true);
    }

    public void TurnOff()
    {
        leftLight.SetActive(false);
        rightLight.SetActive(false);
    }
}
