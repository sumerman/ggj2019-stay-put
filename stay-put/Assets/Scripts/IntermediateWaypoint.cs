using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateWaypoint : Waypoint
{
    public override void onVehicleEnter()
    {
        Debug.Log("Intermediate");
    }

    public override bool allowDriveOn()
    {
        return true;
    }
}
