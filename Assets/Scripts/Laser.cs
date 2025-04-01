using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Laser : MonoBehaviour
    {
        // Explosion prefabs
        [SerializeField] private GameObject laserExplosion;
        //[SerializeField] private GameObject shipExplosion;
        // Laser speed
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
            rb.linearVelocity = laserTransform.up * speed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Agent")
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
    }
}