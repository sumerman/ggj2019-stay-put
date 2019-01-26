using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public Vector3 spawnOffset;

    public Waypoint targetWP;
    public PlayerCharacter pc;
    private LookAt mainCamera;
    //    private Waypoint lastWP;
    private ScreenNotifications notifications;

    private bool stopped;

    private PickupStats inventory;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LookAt>();
        notifications =
                GameObject.FindGameObjectWithTag("UI").GetComponent<ScreenNotifications>();
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
            notifications.SetText("Press \"space\" to leave the car");

            if (Input.GetKeyDown("space"))
            {
                SpawnPlayer();
                notifications.SetText("");
            }
        }
    }

    private void SpawnPlayer()
    {
        pc.Spawn(this.gameObject.transform.position + spawnOffset);
    }

    private void moveTowardsNextWaypoint()
    {
        Vector3 stopPosition = targetWP.transform.position;
        GetComponent<Rigidbody>().position = Vector3.MoveTowards(GetComponent<Rigidbody>().position, stopPosition, speed);
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
        inventory += targetWP.pickup.stats;
        targetWP.onVehicleEnter();
    }

    public void handleWaypoint(Waypoint wp)
    {
        if(wp.allowDriveOn())
        {
            targetWP = wp.getNextWaypoint();
        }
        else
        {
            stop();
        }
    }
}
