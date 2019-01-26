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
    public LookAt mainCamera;
    private Rigidbody rbody;
    //    private Waypoint lastWP;
    private ScreenNotifications notifications;

    private bool stopped;

    private PickupStats inventory;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LookAt>();

        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui) notifications = ui.GetComponent<ScreenNotifications>();

        pc.car = this;
        pc.mainCamera = mainCamera;
        pc.Despawn();

        inventory = GetComponentInChildren<PickupStats>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetWP != null && stopped)
        {
            if (targetWP.allowDriveOn())
            {
                targetWP = targetWP.getNextWaypoint();
                start();
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
            if(notifications!=null) notifications.SetText("Press \"space\" to leave the car");

            if (Input.GetKeyDown("space"))
            {
                GameObject fadeAnimatior = GameObject.FindGameObjectWithTag("FadeAnimations");
                if (fadeAnimatior)
                {
                    CameraChanger changer = fadeAnimatior.GetComponent<CameraChanger>();
                    changer.GetOut();
                }
                else
                {
                    SpawnPlayer();
                }

                if (notifications != null) notifications.SetText("");
            }
        }
    }

    public void SpawnPlayer()
    {
        pc.Spawn(this.gameObject.transform.position + spawnOffset);
    }

    private void moveTowardsNextWaypoint()
    {
        Vector3 spatialDifference = targetWP.gameObject.transform.position - this.rbody.position;
        Quaternion differenceDirection = Quaternion.LookRotation(spatialDifference,transform.up);
        rbody.rotation = (Quaternion.RotateTowards(rbody.rotation, differenceDirection, rotationSpeed*Time.deltaTime));
        rbody.MovePosition(rbody.position + transform.forward * speed * Time.deltaTime);
    }

    private void stop()
    {
        stopped = true;
    }

    private void start()
    {
        stopped = false;
    }

    public void onPlayerEntered()
    {
        mainCamera.SetTarget(this.gameObject);
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
        if (wp.allowDriveOn())
        {
            targetWP = wp.getNextWaypoint();
        }
        else
        {
            inventory.handleWaypoint();
            stop();
        }
    }
}
