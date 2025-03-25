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

        private bool inCooldown;

        private void Awake()
        {
            inCooldown = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Reenable firing
        private void ExitCooldown()
        {
            inCooldown = false;
        }

        // Firing input from the player
        private void OnAttack(InputValue val)
        {
            if (!inCooldown) 
            {
                FireLaser();
            }
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