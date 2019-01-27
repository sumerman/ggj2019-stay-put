using UnityEngine;
using UnityEngine.Events;

public class FadeAnimationPlayer : MonoBehaviour
{
    public Animator animator;

    public UnityEvent FadeComplete;
    public UnityEvent IntroComplete;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartIntro()
    {
        IntroComplete.Invoke();
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
