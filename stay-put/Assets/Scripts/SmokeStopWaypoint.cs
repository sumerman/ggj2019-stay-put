using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeStopWaypoint : Stopwaypoint
{
    public GameObject smoke;
    public SphereCollider coll;

    public override void SpecialCarAction()
    {
        smoke.SetActive(true);
        coll.radius /= 10;
    }
}
