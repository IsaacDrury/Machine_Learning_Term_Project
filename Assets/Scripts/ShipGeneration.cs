using System.Linq;
using System.Xml.Serialization;
using Unity.MLAgents;
using UnityEngine;

public class ShipGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] Team1Agents;
    [SerializeField] GameObject[] Team2Agents;
    [SerializeField] GameObject[] Cruisers;
    [SerializeField] GameObject strikeCraftAgent;
    [SerializeField] GameObject frigateAgent;
    [SerializeField] GameObject cruiserAgent;
    [SerializeField] public int spawnStrikeShips;
    [SerializeField] public int spawnFrigates;
    [SerializeField] public int spawnCruisers;
    public Vector3 zeroRotation = Vector3.zero;
    private Vector3 shipPos1 = Vector3.zero;
    private Vector3 shipPos2 = Vector3.zero;
    private bool isCruiser = false;

    private void randomShipSpawn(GameObject shipAgent, int numShips) {

        for (int i = 0; i < numShips; i++)
        {
            // Instantiate them
            GameObject agent1 = Instantiate(shipAgent);
            agent1.tag = "Team 1";
            // Position them
            // if (shipAgent.GetComponent<Health>().getShipType()) {
            //     isCruiser = true;
            //     generateRandPosRot(numShips, isCruiser);
            // }

            generateRandPosRot(numShips, false);
            agent1.transform.localPosition = shipPos1;
            // Rotato the potatoes
            agent1.transform.Rotate(zeroRotation);

            GameObject agent2 = Instantiate(shipAgent);
            agent2.tag = "Team 2";
            // Setting trail gradient color which fades from front-to-back
            agent1.GetComponent<TrailRenderer>().startColor = new Color (1.0f, 0.4f, 0.0f);
            agent1.GetComponent<TrailRenderer>().startColor = Color.white;
            agent2.transform.localPosition = shipPos2;
            agent2.transform.Rotate(zeroRotation);

            if (!isCruiser) {
                // Save the tiny and medium ships for later
                Team1Agents.Append(agent1);
                Team2Agents.Append(agent2);
            }
            else {
                // We're only tracking one cruiser per team in our map. Don't spawn more...0_o!
                Cruisers.Append(agent1);
                Cruisers.Append(agent2);
            }
        }
    }

    private void generateRandPosRot(int numShips, bool isCruiser) {
        int posX;
        int posY;
        int posZ1;
        int posZ2;
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                for (int k = 0; k < numShips; k++)
                {
                    if (isCruiser) {
                        posX = i * 200 + Random.Range(-850, 850);
                        posY = j * 75 + Random.Range(-850, 850);
                        posZ1 = k + Random.Range(650, 1350);
                        posZ2 = k + Random.Range(-1350, -650);
                        shipPos1 = new Vector3(posX, posY, posZ1);
                        shipPos2 = new Vector3(posX, posY, posZ2);
                    }
                    else {
                        posX = i * 100 + Random.Range(-850, 850);
                        posY = j * 50 + Random.Range(-850, 850);
                        posZ1 = k * 100 + Random.Range(650, 1350);
                        posZ2 = k * 100 + Random.Range(-1350, -650);
                        shipPos1 = new Vector3(posX, posY, posZ1);
                        shipPos2 = new Vector3(posX, posY, posZ2);
                    }
                }
            }
        }
    }

    public void randomShipReset(int numShips, bool isCruiser) {
        for (int i = 0; i < numShips; i++)
        {   
            if (isCruiser) {
                generateRandPosRot(numShips, true);
                Cruisers[i].transform.localPosition = shipPos1;
                Cruisers[i+1].transform.localPosition = shipPos2;
                Cruisers[i].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
                Cruisers[i+1].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
                Cruisers[i].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
                Cruisers[i+1].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
                Cruisers[i].transform.Rotate(zeroRotation);
                Cruisers[i+1].transform.Rotate(zeroRotation);
            }
            else {
                generateRandPosRot(numShips, false);
            }
            // Go to your room!
            Team1Agents[i].transform.localPosition = shipPos1;
            Team2Agents[i].transform.localPosition = shipPos2;
            // Slow Down!
            Team1Agents[i].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
            Team2Agents[i].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
            // Stop twirling!
            Team1Agents[i].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
            Team2Agents[i].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
            // Look them in the eye!
            Team1Agents[i].transform.Rotate(zeroRotation);
            Team2Agents[i].transform.Rotate(zeroRotation);

        }
    }

    void Start()
    {
        randomShipSpawn(strikeCraftAgent, spawnStrikeShips);
        randomShipSpawn(frigateAgent, spawnFrigates);
        randomShipSpawn(cruiserAgent, spawnCruisers);
    } 
}
