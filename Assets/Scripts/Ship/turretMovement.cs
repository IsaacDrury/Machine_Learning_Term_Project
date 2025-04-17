using Unity.Collections;
using UnityEngine;

public class turretMovement : MonoBehaviour
{
    [SerializeField] private float angle = 90.0f;
    [SerializeField] private Transform turretBody;
    [SerializeField] private Transform turretSupport;

    public void ApplyMovement(float tilt, float turn)
    {
        turretBody.transform.Rotate(new Vector3(tilt, 0, 0), angle * Time.deltaTime);
        turretSupport.transform.Rotate(new Vector3(0, -turn, 0), angle * Time.deltaTime);
    }
}
