﻿using System.Collections;
using UnityEngine;
using Unity.MLAgents;

namespace Assets.Scripts
{
    public class Laser : MonoBehaviour
    {
        // Explosion prefabs
        [SerializeField] private GameObject laserExplosion;
        // Laser speed
        [SerializeField] private int speed;

        private Transform laserTransform;
        private Rigidbody rb;
        private Agent parentAgent;
        private bool hit;
        private int laserDamage;

        private void Awake()
        {
            this.GetComponent<Collider>().enabled = false;
            Invoke("ReEnableCollider", 0.1f);
            laserTransform = this.transform;
            rb = this.GetComponent<Rigidbody>();
            hit = false;
        }

        private void FixedUpdate()
        {
            rb.linearVelocity = laserTransform.up * speed;
        }

        private void OnTriggerEnter(Collider collision)
        {
            GameObject target = collision.gameObject;
            if (hit == false && (target.tag == "Team 1" || target.tag == "Team 2"))
            {
                hit = true;
                parentAgent.AddReward(20.0f);
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                target.transform.GetChild(1).GetComponent<Health>().ChangeHealth(laserDamage);
                Destroy(this.gameObject);

            }
            else if (hit == false && collision.gameObject.tag == "Obstacle")
            {
                Instantiate(laserExplosion, laserTransform.position, laserTransform.rotation);
                Destroy(this.gameObject);
            }
        }

        private void ReEnableCollider()
        {
            this.GetComponent<Collider>().enabled = true;
        }

        public void SetAgents(Agent agent)
        {
            parentAgent = agent;
        }

        public void SetDamage(int damage)
        {
            laserDamage = damage;
        }
    }
}