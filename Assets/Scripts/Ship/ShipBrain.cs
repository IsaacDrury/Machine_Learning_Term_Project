using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ShipBrain : Agent
{
    private agentMovement movementScript;
    public float maxSteps = 20000f;
    private float stepCount;
    public RayPerceptionSensorComponent3D sensorFront1;
    public RayPerceptionSensorComponent3D sensorFront2;


    void Awake()
    {
        movementScript = GetComponent<agentMovement>();
    }

    //Currently unused
    /*
    public override void OnEpisodeBegin()
    {

    }
    */

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Laser") || collision.gameObject.CompareTag("Team 1") || collision.gameObject.CompareTag("Team 2"))
        {
            // Add a negative reward for hitting big space rock
            AddReward(-6.0f);

            // Log to console for debugging
            Debug.LogWarning("Ship Hit By Something");
            EndEpisode();
        }
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float pitch = actionBuffers.ContinuousActions[0];
        float yaw = actionBuffers.ContinuousActions[1];
        movementScript.ApplyMovement(pitch, yaw);


        CheckSensorHits(sensorFront1);
        CheckSensorHits(sensorFront2);

        if (stepCount == maxSteps)
        {
            EndEpisode();
        }
        stepCount++;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");   // Pitch (up/down)
        continuousActionsOut[1] = Input.GetAxis("Horizontal"); // Yaw (left/right)
    }

    private void CheckSensorHits(RayPerceptionSensorComponent3D sensor)
    {
        if (sensor != null)
        {
            var perception = RayPerceptionSensor.Perceive(sensor.GetRayPerceptionInput(), false);
            if (perception != null && perception.RayOutputs != null)
            {
                foreach (var rayOutput in perception.RayOutputs)
                {
                    if (rayOutput.HitGameObject != null &&
                        ((gameObject.CompareTag("Team 1") && rayOutput.HitGameObject.CompareTag("Team 2")) ||
                         (gameObject.CompareTag("Team 2") && rayOutput.HitGameObject.CompareTag("Team 1"))))
                    {
                        AddReward(0.001f);
                    }
                }
            }
        }
    }

}
