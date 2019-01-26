using System;
using UnityEngine;

public class PickupStats : MonoBehaviour
{
    const int decreaseDelta = 5;

    public int fuel;
    public int food;
    public int sparePart;

    public static PickupStats operator +(PickupStats ps1, PickupStats ps2)
    {
        ps1.fuel += ps2.fuel;
        ps1.food += ps2.food;
        ps1.sparePart += ps2.sparePart;
        return ps1;
    }

    public void handleWaypoint()
    {
        fuel = Math.Max(fuel - decreaseDelta, 0);
        food = Math.Max(food - decreaseDelta, 0);
        sparePart = Math.Max(sparePart - decreaseDelta, 0);
    }
}
