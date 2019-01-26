using System;
namespace Application
{
    public enum PickupType
    {
        Fuel = 0,
        Food,
        SparePart
    }

    public struct PickupStats
    {
        public PickupType type;
        public int amount;
    }
}
