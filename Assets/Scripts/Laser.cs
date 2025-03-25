using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private int speed;
        private Transform laserTransform;
        private Rigidbody rb;

        private void Awake()
        {
            laserTransform = this.transform;
            rb = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = laserTransform.forward * speed;
        }

        // Initializes rotation and velocity of a laser shot from a ship
        // Uses a reference to the ship's transform.
        public void InitializeLazer(Transform parentTransform)
        {
            laserTransform.rotation = parentTransform.rotation;
        }
    }
}