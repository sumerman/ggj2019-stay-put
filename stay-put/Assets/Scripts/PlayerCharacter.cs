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

    private Rigidbody rbody;
    private bool onSpawnFrame = true;
    private bool canEnterCar = false;

    // Start is called before the first frame update
    void Start()
    {
        rbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xTrans = Input.GetAxis("Vertical") * walkingSpeed;
        float zTrans = -Input.GetAxis("Horizontal") * walkingSpeed;
        rbody.MovePosition(rbody.position + new Vector3(xTrans, 0, zTrans));
        if (Input.GetKeyDown("e") && canEnterCar)
        //Debug.Log("Hello");
        if (Input.GetKeyDown("e"))
        {
            car.onPlayerEntered();
            Despawn();
        }
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
        canEnterCar = true;
    }

    public void DisableCarEnter()
    {
        Debug.Log("No more entering!");
        canEnterCar = false;
    }

    public void onPickedUpObject()
    {
        Debug.Log("picked up");
    }
}
