using Unity.Collections;
using UnityEngine;

public class turretMovement : MonoBehaviour
{
    [SerializeField] private float angle = 90.0f;
    [SerializeField] private Transform turret1Body;
    [SerializeField] private Transform turret1Support;
    [SerializeField] private Transform turret2Body;
    [SerializeField] private Transform turret2Support;
    public void ApplyMovement(float tilt, float turn)
    {
        turret1Body.transform.Rotate(new Vector3(tilt, 0, 0), angle * Time.deltaTime);
        turret1Support.transform.Rotate(new Vector3(0, -turn, 0), angle * Time.deltaTime);
        turret2Body.transform.Rotate(new Vector3(tilt, 0, 0), angle * Time.deltaTime);
        turret2Support.transform.Rotate(new Vector3(0, -turn, 0), angle * Time.deltaTime);
    }
}
