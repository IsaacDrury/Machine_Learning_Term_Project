using System.Numerics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private float movementX;
    private float movementZ;
    public float angle = 90.0f;
    public float thrust = 5.0f;
    public float speed = 50;
    public Rigidbody rBdy;

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
       

        // Experimental ai input code

        /*
        this.transform.forward = vector * speed * Time.deltaTime;

        // The neural net (i.e. policy) has generated output
		Vector3 controlSignal = Vector3.zero;
        vector = controlSignal.x;
		float angle = actionBuffers.ContinuousActions[1];

		arBdy.AddRelativeForce(vector);
        */
    }
}
