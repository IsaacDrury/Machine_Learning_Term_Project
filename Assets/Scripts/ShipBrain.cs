using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ShipBrain : Agent
{
    private agentMovement movementScript;
    public float maxSteps = 50000f;
    private float stepCount;
    void Start()
    {
        movementScript = GetComponent<agentMovement>();

    }
    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Border"))
        {
            // Add a negative reward for hitting big space rock
            AddReward(-6.0f);

            // Log to console for debugging
            Debug.Log("Asteroid / Border Hit Penalty: " + -6.0f);
            EndEpisode();
        }
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(0.0001f);
        float pitch = actionBuffers.ContinuousActions[0];
        float yaw = actionBuffers.ContinuousActions[1];
        movementScript.ApplyMovement(pitch, yaw);
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
