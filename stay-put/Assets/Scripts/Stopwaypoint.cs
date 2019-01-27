using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwaypoint : Waypoint
{
    private bool driveOn = false;

    public Pickup pickup;

    void FixedUpdate()
    {
        /*if (countTime)
        {
            timer += Time.deltaTime;
        }
        if (timer >= waitTime)
        {
            countTime = false;
            timer = 0f;
            driveOn = true;
        }*/
    }

    public override void onVehicleEnter()
    {
        Debug.Log(pickup.gameObject.activeSelf);
        if (!pickup.gameObject.activeSelf)
        {
            driveOn = true;
        }
    }

    public override bool allowDriveOn()
    {
        return driveOn;
    }
}
