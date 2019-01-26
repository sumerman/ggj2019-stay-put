using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;

    public Waypoint targetWP;
    public PlayerCharacter pc;
    //    private Waypoint lastWP;

    private bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        pc.car = this;
        pc.gameObject.SetActive(false);
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
        if (stopped && !pc.gameObject.activeSelf && Input.GetKeyDown("space"))
        {
            Debug.Log("SPACE");
            pc.gameObject.SetActive(true);
            //pc.transform.position = this.transform.position;
        }
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
