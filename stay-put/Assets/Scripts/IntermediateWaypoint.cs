using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateWaypoint : Waypoint
{
    public override void onVehicleEnter()
    {
        
    }

    public override bool allowDriveOn()
    {
        return true;
    }
}
