using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class TurretBrain : Agent
{
    private turretMovement movementScript;
    public float maxSteps = 20000f;
    private float stepCount;
    public RayPerceptionSensorComponent3D sensorFront;

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
        continuousActionsOut[0] = Input.GetAxis("Vertical");   // Tilt (up/down)
        continuousActionsOut[1] = Input.GetAxis("Horizontal"); // Turn (left/right)
    }
}
