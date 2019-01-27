using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsAnimationPlayer : MonoBehaviour
{
    public Animator animator;
    public Car car;

    // Start is called before the first frame update
    void Start()
    {
        // set animation speed relatively to car movement
        if (car && animator)
        {
            animator.SetFloat("speedMultiplier", car.speed/100.0f);
        }
    }
}
