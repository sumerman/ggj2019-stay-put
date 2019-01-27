using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float walkingSpeed;
    public float rotationSpeed;
    public float animationSpeed;
    [HideInInspector]
    public Car car;
    [HideInInspector]
    public LookAt mainCamera;

    private ScreenNotifications notifications;
    private Rigidbody rbody;
    private Animator animator;
    private bool onSpawnFrame = true;
    private bool canEnterCar = false;

    void Awake()
    {
        Debug.Log("wake");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LookAt>();
        rbody = gameObject.GetComponent<Rigidbody>();
        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui) notifications = ui.GetComponent<ScreenNotifications>();
        animator = GetComponent<Animator>();
        if (animator)
        {
            animator.speed = animationSpeed;
            //animator.keepAnimatorControllerStateOnDisable = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        DistanceDependentActions();
        AdjustAnimation();
        if (Input.GetKeyDown("space") && canEnterCar)
        {
            GameObject fadeAnimatior = GameObject.FindGameObjectWithTag("FadeAnimations");
            if (fadeAnimatior)
            {
                Debug.Log("FadeTree");
                FadeAnimationPlayer changer = fadeAnimatior.GetComponent<FadeAnimationPlayer>();
                changer.FadeComplete.AddListener(EnterCar);
                changer.StartFading();
            }
            else
            {
                EnterCar();
            }
        }
    }

    public void EnterCar()
    {
        Debug.Log("olleH");
        car.onPlayerEntered();
        Despawn();
    }

    private void DistanceDependentActions()
    {
        //Debug.Log(Vector3.Distance(gameObject.transform.position, car.gameObject.transform.position));
    }

    private void AdjustAnimation()
    {
        if (animator)
        {
            if (Mathf.Approximately(Input.GetAxis("Horizontal"), 0.0f) &&
                Mathf.Approximately(Input.GetAxis("Vertical"), 0.0f))
            {
                animator.Play("Walk", 0, 0);
                animator.enabled = false;
            }
            else
            {
                animator.enabled = true;
                animator.Play("Walk");
            }
        }
    }

    private void Move()
    {
        float xTrans = -Input.GetAxis("Horizontal") * walkingSpeed * Time.deltaTime;
        float zTrans = -Input.GetAxis("Vertical") * walkingSpeed * Time.deltaTime;
        Vector3 movementDir = new Vector3(xTrans, 0, zTrans);
        if (movementDir.magnitude > 0) { 
            Quaternion differenceDirection = Quaternion.LookRotation(movementDir, transform.up);
            rbody.rotation = (Quaternion.RotateTowards(rbody.rotation, differenceDirection, rotationSpeed * Time.deltaTime));
        }
        rbody.MovePosition(rbody.position + movementDir);
    }

    void LateUpdate()
    {
        onSpawnFrame = false;
    }

    public bool IsOnSpawnFrame()
    {
        return onSpawnFrame;
    }

    public void Spawn(Vector3 position)
    {
        mainCamera.gameObject.SetActive(true);
        this.gameObject.transform.position = position;
        this.gameObject.SetActive(true);
        onSpawnFrame = true;
    }

    public void Despawn()
    {
        DisableCarEnter();
        mainCamera.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void EnableCarEnter()
    {
        Debug.Log("good to enter!");
        //if (notifications) notifications.SetText(" ");
        canEnterCar = true;
    }

    public void DisableCarEnter()
    {
        Debug.Log("No more entering!");
        //if (notifications) notifications.DisableNotification();
        canEnterCar = false;
    }
}
