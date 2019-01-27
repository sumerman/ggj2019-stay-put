using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeStopWaypoint : Stopwaypoint
{
    public GameObject smoke;
    public SphereCollider coll;
    public GlitchController glitcher;

    public override void SpecialCarAction()
    {
        smoke.SetActive(true);
        coll.radius /= 4;
        glitcher.enableCrash = true;
    }
}
