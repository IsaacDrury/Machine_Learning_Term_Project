using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class agentMovement : MonoBehaviour
{
    public float angle = 90.0f;
    public float speed = 50f;
    public Rigidbody rBdy;

    private Health health;

    private void Awake()
    {
        health = this.gameObject.transform.GetChild(1).GetComponent<Health>();
    }

    // Apply rotation and velocity
    public void ApplyMovement(float pitch, float yaw)
    {
        transform.Rotate(new Vector3(pitch, 0, -yaw), angle * Time.deltaTime);
        rBdy.linearVelocity = transform.forward * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // Collision with other ships
        if (collision.gameObject.tag == "Team 1" || collision.gameObject.tag == "Team 2")
        {
            Health otherHealth = collision.gameObject.transform.GetChild(1).GetComponent<Health>();
            if (otherHealth.GetShipType() == "Strike-craft") 
            {
                health.ChangeHealth(4);
            }
            else if (otherHealth.GetShipType() == "Frigate")
            {
                health.ChangeHealth(12);
            }
            else if (otherHealth.GetShipType() == "Cruiser")
            {
                health.ChangeHealth(24);
            }
        }
        // Collisions with asteroids and world borders
        else if (collision.gameObject.tag == "Obstacle")
        {
            if (collision.gameObject.transform.parent.name == "WorldBorder")
            {
                health.ChangeHealth(200);
            }
            else if (health.GetShipType() != "Cruiser")
            {
                health.ChangeHealth(6);
            }
        }
    }
}
