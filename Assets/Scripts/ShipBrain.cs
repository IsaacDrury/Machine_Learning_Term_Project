using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ShipBrain : Agent
{
    [SerializeField] private Vector3 startingPosition;
    private agentMovement movementScript;
    public float maxSteps = 20000f;
    private float stepCount;
    public RayPerceptionSensorComponent3D sensorFront;

    void Start()
    {
        movementScript = GetComponent<agentMovement>();
 
    }
    public override void OnEpisodeBegin()
    {
        transform.position = startingPosition;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Laser") || other.CompareTag("Agent"))
        {
            // Add a negative reward for hitting big space rock
            AddReward(-6.0f);

            // Log to console for debugging
            Debug.LogWarning("Ship Destroyed");
            EndEpisode();
        }
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float pitch = actionBuffers.ContinuousActions[0];
        float yaw = actionBuffers.ContinuousActions[1];
        movementScript.ApplyMovement(pitch, yaw);

        if (sensorFront != null)
        {
            var perception = RayPerceptionSensor.Perceive(sensorFront.GetRayPerceptionInput(), false);
            if (perception != null)
            {
                var rayOutputs = perception.RayOutputs;

                foreach (var rayOutput in rayOutputs)
                {
                    if (rayOutput.HitGameObject != null && rayOutput.HitGameObject.CompareTag("Agent"))
                    {
                        AddReward(0.01f);
                        //Debug.LogWarning("Ship Seen");
                    }
                }
            }
        }

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
}
