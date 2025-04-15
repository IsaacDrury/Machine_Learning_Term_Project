using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class cruiserMovement : MonoBehaviour
{
    [SerializeField] private float angle = 90.0f;
    [SerializeField] private float speed = 50f;
    [SerializeField] private Rigidbody rBdy;

    private Health health;

    private void Awake()
    {
        health = this.gameObject.transform.GetChild(1).GetComponent<Health>();
    }

    void FixedUpdate()
    {
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
