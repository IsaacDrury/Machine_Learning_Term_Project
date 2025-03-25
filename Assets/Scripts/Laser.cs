using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private GameObject laserExplosion;
        [SerializeField] private GameObject shipExplosion;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "agent")
            {
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                //Instantiate(shipExplosion, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                Destroy(this.gameObject);

            }
            else if (collision.gameObject.tag == "Obstacle")
            {
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                Destroy(this.gameObject);
            }
        }

        // Initializes rotation and velocity of a laser shot from a ship
        // Uses a reference to the ship's transform.
        public void InitializeLazer(Transform parentTransform)
        {
            laserTransform.rotation = parentTransform.rotation;
        }
    }
}