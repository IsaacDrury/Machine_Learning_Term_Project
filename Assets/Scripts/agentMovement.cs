using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class agentMovement : MonoBehaviour
{
    public float angle = 90.0f;
    public float speed = 50f;
    public Rigidbody rBdy;

    public void ApplyMovement(float pitch, float yaw)
    {
        transform.Rotate(new Vector3(pitch, 0, -yaw), angle * Time.deltaTime);
        // Set forward velocity
        rBdy.linearVelocity = transform.forward * speed;
    }
}
