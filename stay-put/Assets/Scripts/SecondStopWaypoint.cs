using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStopWaypoint : Stopwaypoint
{
    public DirectionIndicator indicator;
    public LightFlickerController lightController;

    public override void SpecialCarAction()
    {
        indicator.gameObject.SetActive(true);
    }

    public override bool allowDriveOn()
    {
        if (driveOn)
        {
            lightController.TurnLeftOn();
        }
        return driveOn;
    }
}
