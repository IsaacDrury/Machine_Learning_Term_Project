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
        private ShipBrain parentAgent;

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
            GameObject target = collision.gameObject;
            if (target.tag == "Team 1" || target.tag == "Team 2")
            {
                parentAgent.AddReward(5.0f);
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                target.transform.GetChild(1).GetComponent<Health>().ChangeHealth(1);
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

        public void SetAgents(ShipBrain agent)
        {
            parentAgent = agent;
        }
    }
}