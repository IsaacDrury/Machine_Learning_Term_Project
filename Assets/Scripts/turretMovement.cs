using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class turretMovement : MonoBehaviour
{
    [SerializeField] private float angle = 90.0f;
    [SerializeField] private GameObject turret1;
    [SerializeField] private GameObject turret2;

    public void ApplyMovement(float tilt, float turn)
    {
        turret1.transform.Rotate(new Vector3(tilt, -turn, 0), angle * Time.deltaTime);
        turret2.transform.Rotate(new Vector3(tilt, -turn, 0), angle * Time.deltaTime);
    }
}
