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

    public void ApplyMovement(float pitch, float yaw)
    {
        transform.Rotate(new Vector3(pitch, 0, -yaw), angle * Time.deltaTime);
        // Set forward velocity
        rBdy.linearVelocity = transform.forward * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
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
        else if (collision.gameObject.tag == "Obstacle" && health.GetShipType() != "Cruiser")
        {
            health.ChangeHealth(6);
        }
    }
}
