using System.Numerics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private float movementX;
    private float movementZ;
    private float momentumX;
    private float momentumZ;
    public float angle = 10.0f;
    public float thrust = 5.0f;
    public float speed = 10;
    public Rigidbody rBdy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnMove(InputValue movementValue) {
        UnityEngine.Vector2 movementVector = movementValue.Get<UnityEngine.Vector2>();
        // Debug.Log(movementVector.x + ", " + movementVector.y);
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }
    //Spacebar for "boost"
    void OnJump() {
        UnityEngine.Vector3 vector = new UnityEngine.Vector3(0.0f, 0.0f, thrust);
        this.transform.forward = vector * speed * Time.deltaTime;

    //     boost += 0.5f;
    //     Vector3 vector = new Vector3(0.0f, 0.0f, boost);
    //     rBdy.AddForce(vector * speed);
    }
    //C for "crouch"
    // void OnCrouch() {
    //     brake -= 0.5f;
    //     Vector3 vector = new Vector3(0.0f, 0.0f, brake);
    //     rBdy.AddForce(vector * speed);
    // }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotation inputs (i.e. input values 0 and 1 for the ai's input buffer)
        if (movementX != 0 && movementX <= 1 && movementX >= -1) {
            momentumX = movementX;
            this.transform.RotateAround(this.transform.position, this.transform.up, momentumX * angle * Time.deltaTime);
        }
        if (movementZ != 0 && movementZ <= 1 && movementZ >= -1) {
            momentumZ = movementZ;
            this.transform.RotateAround(this.transform.position, this.transform.right, momentumZ * angle * Time.deltaTime);
        }
        UnityEngine.Vector3 vector = new UnityEngine.Vector3(0.0f, 0.0f, thrust);
        //Constant Thrust
        this.transform.forward = vector * speed * Time.deltaTime;

    }
}
