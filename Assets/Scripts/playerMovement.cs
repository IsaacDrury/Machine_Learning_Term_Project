using System.Numerics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public float angle = 90.0f;
    public float speed = 50;
    public Rigidbody rBdy;

    private float movementX;
    private float movementZ;

    //Get player input
    void OnMove(InputValue movementValue) {
        UnityEngine.Vector2 movementVector = movementValue.Get<UnityEngine.Vector2>();
        movementX = movementVector.y;
        movementZ = movementVector.x;
    }

    void FixedUpdate()
    {
        //Rotate and move the ship
        this.transform.Rotate(new UnityEngine.Vector3(movementX, 0, -movementZ), angle * Time.deltaTime);
        rBdy.linearVelocity = this.transform.forward * speed;
    }
}
