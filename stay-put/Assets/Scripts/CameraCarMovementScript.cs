using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCarMovementScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private AudioSource audioSource;
    private Vector3 relativePosition;
    // Start is called before the first frame update
    void Awake()
    {
        relativePosition = gameObject.transform.position - target.gameObject.transform.position;
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + relativePosition;
        transform.LookAt(target);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    public void SwitchTo()
    {
        gameObject.SetActive(true);
        if (audioSource) audioSource.UnPause();
    }

    public void SwitchFrom()
    {
        if (audioSource) audioSource.Pause();
        gameObject.SetActive(false);
    }
}
