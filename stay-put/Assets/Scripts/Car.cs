using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Vector3 spawnOffset;

    public Waypoint targetWP;
    public PlayerCharacter pc;
    public DriftCamera mainCamera;
    private Rigidbody rbody;
    //    private Waypoint lastWP;
    private ScreenNotifications notifications;
    public MotorSoundController motorSound;

    private float maxSpeed;
    private bool stopped;
    private bool playStartSoundOnlyOnce = true;
    private bool isInIntro = true;
    private bool tutorialShown = false;

    private PickupStats inventory;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = speed;
        rbody = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("VehicleCamera").GetComponent<DriftCamera>();

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui) notifications = ui.GetComponent<ScreenNotifications>();

        pc.car = this;
        pc.Despawn();

        inventory = GetComponentInChildren<PickupStats>();

        motorSound.DriveSound();
        GameObject fadeAnimatior = GameObject.FindGameObjectWithTag("FadeAnimations");
        if (fadeAnimatior)
        {
            isInIntro = true;
            FadeAnimationPlayer changer = fadeAnimatior.GetComponent<FadeAnimationPlayer>();
            changer.IntroComplete.AddListener(IntroDone);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInIntro) return;

        if (targetWP != null && stopped)
        {
            if (targetWP.allowDriveOn() && playStartSoundOnlyOnce)
            {
                Debug.Log("Change50");
                targetWP = targetWP.getNextWaypoint();
                StartCoroutine(start());
                playStartSoundOnlyOnce = false;
            }
        }
        if (targetWP != null && !stopped)
        {
            moveTowardsNextWaypoint();
            /*if (targetWP.allowMoveOn())
            {
                moveTowardsNextWaypoint();
            }
            else
            {
                lastWP = targetWP;
                targetWP = targetWP.getNextWaypoint();
            }*/
        }
        if (stopped && !pc.gameObject.activeSelf)
        {
            if (notifications != null && !tutorialShown)
            {
                notifications.SetText(" ");
                tutorialShown = true;
            }

            if (Input.GetKeyDown("space"))
            {
                GameObject fadeAnimatior = GameObject.FindGameObjectWithTag("FadeAnimations");
                if (fadeAnimatior)
                {
                    FadeAnimationPlayer changer = fadeAnimatior.GetComponent<FadeAnimationPlayer>();
                    changer.FadeComplete.AddListener(SpawnPlayer);
                    changer.StartFading();
                }
                else
                {
                    SpawnPlayer();
                }
            }
        }
    }
    
    public void IntroDone()
    {
            isInIntro = false;
    }

    public void SpawnPlayer()
    {
        mainCamera.SwitchFrom();
        pc.Spawn(this.gameObject.transform.position + spawnOffset);
        if (notifications != null) notifications.DisableNotification();
    }

    private void moveTowardsNextWaypoint()
    {
        if (!stopped)
        {
            speed = Mathf.Min(maxSpeed, speed + maxSpeed / 2 * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Max(0, speed - maxSpeed * Time.deltaTime);
        }
        Vector3 spatialDifference = targetWP.gameObject.transform.position - this.rbody.position;
        Quaternion differenceDirection = Quaternion.LookRotation(spatialDifference,transform.up);
        rbody.rotation = (Quaternion.RotateTowards(rbody.rotation, differenceDirection, rotationSpeed*Time.deltaTime));
        rbody.MovePosition(rbody.position + transform.forward * speed * Time.deltaTime);
    }

    private IEnumerator stop()
    {
        speed = 0;
        stopped = true;
        Debug.Log("a");
        motorSound.StopSound();
        Debug.Log("b");
        yield return new WaitForSeconds(motorSound.source.clip.length);
        Debug.Log("c");
    }

    private IEnumerator start()
    {
        motorSound.StartSound();
        yield return new WaitForSeconds(motorSound.source.clip.length);
        motorSound.DriveSound();
        stopped = false;
    }

    public void onPlayerEntered()
    {
        if (notifications != null) notifications.DisableNotification();
        playStartSoundOnlyOnce = true;
        Debug.Log(targetWP);
        mainCamera.SwitchTo();
        Stopwaypoint swp = targetWP as Stopwaypoint;
        if (swp && swp.pickup)
        {
            inventory += swp.pickup.stats;
        }
        targetWP.onVehicleEnter();
    }

    public void handleWaypoint(Waypoint wp)
    {
        Debug.Log(wp.gameObject.ToString() + wp.allowDriveOn());
        wp.SpecialCarAction();
        if (wp.allowDriveOn())
        {
            Debug.Log("Change140");
            targetWP = wp.getNextWaypoint();
        }
        else
        {
            Debug.Log("z");
            inventory.handleWaypoint();
            Debug.Log("y");
            StartCoroutine(stop());
        }
    }
}
