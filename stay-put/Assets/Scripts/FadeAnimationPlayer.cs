using UnityEngine;
using UnityEngine.Events;

public class FadeAnimationPlayer : MonoBehaviour
{
    public Animator animator;

    public UnityEvent FadeComplete;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartFading()
    {
        animator.SetTrigger("LeaveCar");
    }

    public void OnFadeComplete()
    {
        FadeComplete.Invoke();
        FadeComplete.RemoveAllListeners();
    }
}
