using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class ShipGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] Team1Agents;
    [SerializeField] GameObject strikeCraftAgent;
    [SerializeField] GameObject[] Team2Agents;
    [SerializeField] int spawnShips;
    public Vector3 T1Rotation = Vector3.zero;
    public Vector3 T2Rotation = Vector3.zero;
    private Vector3 shipPos1 = Vector3.zero;
    private Vector3 shipPos2 = Vector3.zero;

    private void randomShipSpawn() {

        for (int i = 0; i < spawnShips; i++)
        {
            //Instantiate them
            GameObject agent1 = Instantiate(strikeCraftAgent);
            agent1.tag = "Team 1";
            //Position them
            generateRandPosRot();
            agent1.transform.localPosition = shipPos1;
            // Rotato the potatoes
            agent1.transform.Rotate(T1Rotation);

            GameObject agent2 = Instantiate(strikeCraftAgent);
            agent2.tag = "Team 2";
            agent2.transform.localPosition = shipPos2;
            agent2.transform.Rotate(T2Rotation);

            //Save them for later
            Team1Agents.Append(agent1);
            Team2Agents.Append(agent2);
        }
    }

    private void generateRandPosRot() {
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                for (int k = 0; k < spawnShips; k++)
                {
                    int posX = i * 100 + Random.Range(-850, 850);
                    int posY = j * 50 + Random.Range(-850, 850);
                    int posZ1 = k * 100 + Random.Range(650, 1350);
                    int posZ2 = k * 100 + Random.Range(-1350, -650);
                    shipPos1 = new Vector3(posX, posY, posZ1);
                    shipPos2 = new Vector3(posX, posY, posZ2);
                }
            }
        }
    }

    public void randomShipReset() {
        for (int i = 0; i < spawnShips; i++)
        {
            // Go to your room!
            generateRandPosRot();
            Team1Agents[i].transform.localPosition = shipPos1;
            Team2Agents[i].transform.localPosition = shipPos2;
            // Slow Down!
            Team1Agents[i].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
            Team2Agents[i].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
            // Stop twirling!
            Team1Agents[i].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
            Team2Agents[i].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
            // Look them in the eye!
            Team1Agents[i].transform.Rotate(T1Rotation);
            Team2Agents[i].transform.Rotate(T2Rotation);

        }
    }

    void Start()
    {
        randomShipSpawn();
    } 
}
