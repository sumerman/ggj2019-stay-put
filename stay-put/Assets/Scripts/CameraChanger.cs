using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Animator animator;

    public Car car;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetOut()
    {
        animator.SetTrigger("LeaveCar");
        // Add sound - how?
    }

    public void OnFadeComplete()
    {
        if (car) car.SpawnPlayer();
    }
}
