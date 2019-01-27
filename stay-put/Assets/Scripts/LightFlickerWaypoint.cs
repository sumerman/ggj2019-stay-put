using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerWaypoint : IntermediateWaypoint
{
    public LightFlickerController lightController;

    public override void SpecialCarAction ()
    {
        StartCoroutine(lightController.StartBreaking());
    }
}
