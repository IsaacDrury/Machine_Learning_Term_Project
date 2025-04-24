using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Unity.MLAgents;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Diagnostics;
using System;

namespace Assets.Scripts
{
    public class LaserFiring : MonoBehaviour
    {
        // Laser Prefab
        [SerializeField] private GameObject team1Laser;
        [SerializeField] private GameObject team2Laser;
        [SerializeField] private AudioSource sound;
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private float delay;

        private Ray ray;
        private RaycastHit hit;
        private bool inCooldown;
        private int index;
        private Agent agent;

        private void Awake()
        {
            inCooldown = false;
            agent = GetComponent<Agent>();
            if (agent == null)
            {
                agent = GetComponentInParent<Agent>();
            }
        }

        private void Update()
        {
            ray = new Ray(firingPoint.position, firingPoint.forward);
            UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 500);
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if ((hit.collider.tag == "Team 1" && this.gameObject.tag != "Team 1" && hit.distance < 500)
                    || (hit.collider.tag == "Team 2" && this.gameObject.tag != "Team 2" && hit.distance < 500))
                {
                    //Add reward here
                    if (!inCooldown)
                    {
                        agent.AddReward(10.0f);
                        UnityEngine.Debug.LogWarning("Firing Laser");

                        FireLaser();
                    }
                }
            }
        }

        // Reenable firing
        private void ExitCooldown()
        {
            inCooldown = false;
        }

        // Instantiate laser
        public void FireLaser()
        {
            if (clips == null || clips.Length == 0)
            {
                UnityEngine.Debug.LogWarning($"{name} — No audio clips assigned. Skipping sound.");
            }
            else
            {
                index = UnityEngine.Random.Range(0, clips.Length);
                sound.clip = clips[index];
                sound.Play();
            }
            inCooldown = true;
            GameObject laserProjectile = null;
            if (this.tag == "Team 1")
            {
                laserProjectile = Instantiate(team1Laser, firingPoint.position, firingPoint.rotation);
                if (this.transform.GetChild(1).GetComponent<Health>().GetShipType() == "Stike-craft")
                {
                    laserProjectile.GetComponent<Laser>().SetDamage(1);
                }
                else
                {
                    laserProjectile.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    laserProjectile.GetComponent<Laser>().SetDamage(4);
                }
            }
            else
            {
                laserProjectile = Instantiate(team2Laser, firingPoint.position, firingPoint.rotation);
            }
            laserProjectile.transform.Rotate(new UnityEngine.Vector3(1, 0, 0), 90);
            Laser laserScript = laserProjectile.GetComponent<Laser>();
            laserScript.SetAgents(agent);
            Invoke("ExitCooldown", delay);
        }
    }
}