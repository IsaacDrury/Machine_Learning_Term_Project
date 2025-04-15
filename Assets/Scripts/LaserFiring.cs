using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class LaserFiring : MonoBehaviour
    {
        // Laser Prefab
        [SerializeField] private GameObject laser;
        [SerializeField] private AudioSource sound;
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private Transform firingPoint;
        [SerializeField] private float delay;

        private Ray ray;
        private RaycastHit hit;
        private bool inCooldown;
        private int index;
        private ShipBrain agent;

        private void Awake()
        {
            inCooldown = false;
            agent = GetComponent<ShipBrain>();
        }

        private void Update()
        {
            ray = new Ray(firingPoint.position, firingPoint.forward);
            Debug.DrawRay(ray.origin, ray.direction * 500);
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
                        Debug.LogWarning("Firing Laser");

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
            index = Random.Range(0, clips.Length);
            sound.clip = clips[index];
            sound.Play();
            inCooldown = true;
            GameObject laserProjectile = Instantiate(laser, firingPoint.position, firingPoint.rotation);
            laserProjectile.transform.Rotate(new UnityEngine.Vector3(1, 0, 0), 90);
            Laser laserScript = laserProjectile.GetComponent<Laser>();
            laserScript.SetAgents(agent);
            Invoke("ExitCooldown", delay);
        }
    }
}