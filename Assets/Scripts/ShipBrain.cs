using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ShipBrain : Agent
{
    private agentMovement movementScript;
    private turretMovement turretScript;
    public float maxSteps = 20000f;
    private float stepCount;
    public RayPerceptionSensorComponent3D sensorFront;

    void Start()
    {
        if (this.gameObject.transform.GetChild(1).GetComponent<Health>().GetShipType() == "Cruiser") {
            turretScript = GetComponent<turretMovement>();
        }
        movementScript = GetComponent<agentMovement>();
    }
    public override void OnEpisodeBegin()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Laser") || other.CompareTag("Team 1") || other.CompareTag("Team 2") || other.CompareTag("Border"))
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
        if (this.gameObject.transform.GetChild(1).GetComponent<Health>().GetShipType() == "Cruiser") {
            float tilt = actionBuffers.ContinuousActions[0];
            float turn = actionBuffers.ContinuousActions[1];
            turretScript.ApplyMovement(tilt, turn);
        }
        else {
            float pitch = actionBuffers.ContinuousActions[0];
            float yaw = actionBuffers.ContinuousActions[1];
            movementScript.ApplyMovement(pitch, yaw);
        }

        if (sensorFront != null)
        {
            var perception = RayPerceptionSensor.Perceive(sensorFront.GetRayPerceptionInput(), false);
            if (perception != null && perception.RayOutputs != null)
            {
                var rayOutputs = perception.RayOutputs;
                foreach (var rayOutput in rayOutputs)
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
