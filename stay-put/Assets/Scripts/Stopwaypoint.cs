using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwaypoint : Waypoint
{
    public float waitTime = 10f;

    private float timer = 0f;
    private bool countTime = false;
    private bool doonce = true;
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
        driveOn = true;
        /*if (doonce)
        {
            doonce = false;
            countTime = true;
        }*/
    }

    public override bool allowDriveOn()
    {
        return driveOn;
    }
}
