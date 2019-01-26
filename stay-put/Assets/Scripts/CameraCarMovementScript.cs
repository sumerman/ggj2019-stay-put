using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCarMovementScript : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public AudioSource audioSource;
    private Vector3 relativePosition;
    private float gameTameOnDisable = 0;
    private float trackTimeOnDisable;

    // Start is called before the first frame update
    void Awake()
    {
        relativePosition = gameObject.transform.position - target.gameObject.transform.position;
        //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
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
        float timeElapsed = Time.fixedTime - gameTameOnDisable;
        audioSource.time = trackTimeOnDisable + timeElapsed;
        //if (audioSource) audioSource.mute = false;
        audioSource.Play();
    }

    public void SwitchFrom()
    {
        gameTameOnDisable = Time.fixedTime;
        trackTimeOnDisable = audioSource.time;
        //if (audioSource) audioSource.mute = true;
        gameObject.SetActive(false);
    }
}
