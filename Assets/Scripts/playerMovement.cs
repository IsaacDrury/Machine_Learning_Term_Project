using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private float movementX;
    private float movementZ;
    private float boost;
    private float brake;
    public Rigidbody rBdy;
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementZ = movementVector.y;
    }
    //Spacebar for "boost"
    void OnJump() {
        boost -= 0.5f;
        Vector3 vector = new Vector3(boost, 0.0f, 0.0f);
        rBdy.AddForce(vector * speed);
    }
    void OnCrouch() {
        brake += 0.5f;
        Vector3 vector = new Vector3(brake, 0.0f, 0.0f);
        rBdy.AddForce(vector * speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.RotateAround(this.transform.position, this.transform.up, -movementX);
        this.transform.RotateAround(this.transform.position, this.transform.forward, movementZ);
        // Vector3 vector = new Vector3(movementX,0.0f,movementZ);
        // rBdy.AddForce(vector * speed);

    }
}
