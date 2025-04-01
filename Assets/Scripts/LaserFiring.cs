using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class LaserFiring : MonoBehaviour
    {
        // Laser Prefab
        [SerializeField] private GameObject laser;
        // Transform that will be used to instantiate the laser at the appropriate spot
        [SerializeField] private Transform firingPoint;
        // Delay between lasers fired
        [SerializeField] private float delay;

        private Ray ray;
        private RaycastHit hit;
        private bool inCooldown;

        private void Awake()
        {
            inCooldown = false;
        }

        private void Update()
        {
            ray = new Ray(firingPoint.position, firingPoint.forward);
            Physics.Raycast(ray, out hit);
            if (hit.collider.tag == "Agent" && hit.distance < 500)
            {
                FireLaser();
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
            inCooldown = true;
            Instantiate(laser, firingPoint.position, firingPoint.rotation);
            Invoke("ExitCooldown", delay);
        }
    }
}