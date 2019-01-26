using System;
using UnityEngine;

public class PickupStats : MonoBehaviour
{
    public int Fuel;
    public int Food;
    public int SparePart;

    public static PickupStats operator +(PickupStats ps1, PickupStats ps2)
    {
        ps1.Fuel += ps2.Fuel;
        ps1.Food += ps2.Food;
        ps1.SparePart += ps2.SparePart;
        return ps1;
    }
}
