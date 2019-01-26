using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float walkingSpeed;
    [HideInInspector]
    public Car car;
    [HideInInspector]
    public LookAt mainCamera;

    private ScreenNotifications notifications;
    private Rigidbody rbody;
    private bool onSpawnFrame = true;
    private bool canEnterCar = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui) notifications = ui.GetComponent<ScreenNotifications>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        DistanceDependentActions();
        if (Input.GetKeyDown("space") && canEnterCar)
        {
            car.onPlayerEntered();
            Despawn();
        }
    }

    private void DistanceDependentActions()
    {
        Debug.Log(Vector3.Distance(gameObject.transform.position, car.gameObject.transform.position));
    }

    private void Move()
    {
        float xTrans = Input.GetAxis("Vertical") * walkingSpeed;
        float zTrans = -Input.GetAxis("Horizontal") * walkingSpeed;
        rbody.MovePosition(rbody.position + new Vector3(xTrans, 0, zTrans));
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
        this.gameObject.transform.position = position;
        mainCamera.SetTarget(this.gameObject);
        this.gameObject.SetActive(true);
        onSpawnFrame = true;
    }

    public void Despawn()
    {
        DisableCarEnter();
        this.gameObject.SetActive(false);
    }

    public void EnableCarEnter()
    {
        Debug.Log("good to enter!");
        if (notifications) notifications.SetText("Press \"space\" to enter the car");
        canEnterCar = true;
    }

    public void DisableCarEnter()
    {
        Debug.Log("No more entering!");
        if (notifications) notifications.SetText("");
        canEnterCar = false;
    }
}
