using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform tf;

    private int sprint;
    private int upDir;
    private float rightDir;
    private float forwardDir;
    private float pitch;
    private float yaw;
    private bool toggle;

    void Awake()
    {
        sprint = 1;
        upDir = 0;
        rightDir = 0;
        forwardDir = 0;
        pitch = 0;
        yaw = 0;
        toggle = false;
    }

    private void FixedUpdate()
    {
        if (toggle)
        {
            rb.linearVelocity = tf.forward * 100 * forwardDir * sprint + tf.right * 100 * rightDir * sprint + new Vector3(0, upDir, 0) * sprint;
            tf.Rotate(pitch, yaw, 0);
        }
    }

    private void OnMove(InputValue val)
    {
        Vector2 dirs = val.Get<Vector2>();
        forwardDir = dirs.y;
        rightDir = dirs.x;
    }

    private void OnLook(InputValue val)
    {
        Vector2 dirs = val.Get<Vector2>();
        Debug.Log(dirs);
        pitch = -dirs.y;
        yaw = dirs.x;
    }

    private void OnSprint(InputValue val)
    {
        if (val.Get<float>() == 1) 
        {
            sprint = 5;
        }
        else
        {
            sprint = 1;
        }
    }
    private void OnUp(InputValue val)
    {
        if (val.Get<float>() == 1)
        {
            upDir += 100;
        }
        else
        {
            upDir -= 100;
        }
    }

    private void OnDown(InputValue val)
    {
        if (val.Get<float>() == 1)
        {
            upDir -= 100;
        }
        else
        {
            upDir += 100;
        }
    }

    private void OnToggle()
    {
        toggle = !toggle;
        if (toggle)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
        }
    }
}
