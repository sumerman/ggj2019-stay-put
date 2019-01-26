using UnityEngine;

public abstract class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;

    public Pickup pickup;

    public virtual Waypoint getNextWaypoint()
    {
        return nextWaypoint;
    }

    void OnTriggerEnter(Collider other)
    {
        Car car = other.gameObject.GetComponent<Car>();
        if (car == null) return;
        Debug.Log("Trigger entered");
        car.handleWaypoint(this);
    }

    public abstract void onVehicleEnter();
    public abstract bool allowDriveOn();
}
