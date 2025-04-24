using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Diagnostics;

public class TurretBrain : Agent
{
    private turretMovement movementScript;
    public float maxSteps = 20000f;
    private float stepCount;

    void Awake()
    {
        movementScript = this.GetComponent<turretMovement>();

    }

    //Currently unused
    /*
    public override void OnEpisodeBegin()
    {

    }
    */

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float tilt = actionBuffers.ContinuousActions[0];
        float turn = actionBuffers.ContinuousActions[1];
        movementScript.ApplyMovement(tilt, turn);

        if (stepCount == maxSteps)
        {
            EndEpisode();
        }
        stepCount++;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");   // Tilt (up/down)
        continuousActionsOut[1] = Input.GetAxis("Horizontal"); // Turn (left/right)
    }
}
