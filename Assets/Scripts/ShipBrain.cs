using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class ShipBrain : Agent
{
    [SerializeField] public GameObject ShipGenerator;
    private agentMovement movementScript;
    private ShipGeneration generatorScript;
    public float maxSteps = 20000f;
    private float stepCount;
    public RayPerceptionSensorComponent3D sensorFront;
    private bool start = true;

    void Start()
    {
        movementScript = GetComponent<agentMovement>();
        generatorScript = ShipGenerator.GetComponent<ShipGeneration>();
    }
    public override void OnEpisodeBegin()
    {
        // Ships were already given a random spawn on start by the ShipGenerator so skip the first OnEpisodeBegin
        if (!start) {
            int strikeAgents = generatorScript.spawnStrikeShips;
            int frigateAgents = generatorScript.spawnFrigates;
            int cruiserAgents = generatorScript.spawnCruisers;

            generatorScript.randomShipReset(strikeAgents, false);
            generatorScript.randomShipReset(frigateAgents, false);
            generatorScript.randomShipReset(cruiserAgents, true);
        }
        else {
            start = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Laser") || other.CompareTag("Team 1") || other.CompareTag("Team 2"))
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
