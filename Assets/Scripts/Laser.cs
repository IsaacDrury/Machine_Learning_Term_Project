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
            this.GetComponent<Collider>().enabled = false;
            Invoke("ReEnableCollider", 0.1f);
            laserTransform = this.transform;
            rb = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = laserTransform.up * speed;
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.tag == "Agent")
            {
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                //Subtract health from ship
                Destroy(this.gameObject);

            }
            else if (collision.gameObject.tag == "Obstacle")
            {
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                Destroy(this.gameObject);
            }
        }

        private void ReEnableCollider()
        {
            this.GetComponent<Collider>().enabled = true;
        }
    }
}