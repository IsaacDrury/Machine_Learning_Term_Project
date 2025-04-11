using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Assets.Scripts;
using Unity.MLAgents;
using UnityEngine;

public class ShipGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> Team1Agents;
    [SerializeField] private List<GameObject> Team2Agents;
    [SerializeField] private List<GameObject> Cruisers;
    [SerializeField] GameObject strikeCraftAgent;
    [SerializeField] GameObject frigateAgent;
    [SerializeField] GameObject cruiserAgent;
    [SerializeField] Canvas CameraCanvas;
    public int spawnStrikeShips;
    public int spawnFrigates;
    public int spawnCruisers;
    [SerializeField] Vector3 T1Rotation = Vector3.zero;
    [SerializeField] Vector3 T2Rotation = Vector3.zero;
    private Vector3 shipPos1 = Vector3.zero;
    private Vector3 shipPos2 = Vector3.zero;
    private bool isCruiser = false;

    private void randomShipSpawn(GameObject shipAgent, int numShips) {

        for (int i = 0; i < numShips; i++)
        {
            if (shipAgent.GetComponentInChildren<Health>().GetShipType() == "Cruiser") {
                isCruiser = true;
                generateRandPos(1, isCruiser);
            }
            else {
                generateRandPos(1, false);
            }

            // Instantiate them
            GameObject agent1 = Instantiate(shipAgent);
            agent1.tag = "Team 1";
            // Position them
            agent1.transform.localPosition = shipPos1;
            // Rotato the potatoes
            agent1.transform.Rotate(T1Rotation);

            GameObject agent2 = Instantiate(shipAgent);
            agent2.tag = "Team 2";
            // Setting trail gradient color for Team2 which fades from front-to-back
            agent1.GetComponent<TrailRenderer>().startColor = new Color (1.0f, 0.4f, 0.0f);
            agent1.GetComponent<TrailRenderer>().endColor = Color.white;
            agent2.transform.localPosition = shipPos2;
            agent2.transform.Rotate(T2Rotation);

            if (isCruiser) {
                // We're only tracking one cruiser per team in our map. Don't spawn more...0_o!
                isCruiser = false;
                Cruisers.Add(agent1);
                Cruisers.Add(agent2);
            }
            else {
                // Save the tiny and medium ships for later
                Team1Agents.Add(agent1);
                Team2Agents.Add(agent2);
            }
        }
    }

    private void generateRandPos(int numShips, bool Cruiser) {
        int posX1;
        int posX2;
        int posY1;
        int posY2;
        int posZ1;
        int posZ2;
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                for (int k = 0; k < numShips; k++)
                {
                    if (Cruiser) {
                        posX1 = i * 200 + Random.Range(-850, 850);
                        posX2 = i * 200 + Random.Range(-850, 850);
                        posY1 = j * 75 + Random.Range(-850, 850);
                        posY2 = j * 75 + Random.Range(-850, 850);
                        posZ1 = k + Random.Range(650, 1350);
                        posZ2 = k + Random.Range(-1350, -650);
                        shipPos1 = new Vector3(posX1, posY1, posZ1);
                        shipPos2 = new Vector3(posX2, posY2, posZ2);
                    }
                    else {
                        posX1 = i * 100 + Random.Range(-850, 850);
                        posX2 = i * 100 + Random.Range(-850, 850);
                        posY1 = j * 50 + Random.Range(-850, 850);
                        posY2 = j * 50 + Random.Range(-850, 850);
                        posZ1 = k * 100 + Random.Range(650, 1350);
                        posZ2 = k * 100 + Random.Range(-1350, -650);
                        shipPos1 = new Vector3(posX1, posY1, posZ1);
                        shipPos2 = new Vector3(posX2, posY2, posZ2);
                    }
                }
            }
        }
    }

    public void randomShipReset(int numShips, bool Cruiser) {
        for (int i = 0; i < numShips; i++)
        {   
            // Where we at?
            Debug.Log("Index: " + i + " " + Team1Agents.Count + " " + Team2Agents.Count + " " + Cruisers.Count);

            if (Cruiser) {
                generateRandPos(1, true);
                Cruisers[i].SetActive(true);
                // Check the first cruisers tag and reset team-specific variables
                if (Cruisers[i].tag == "Team 1") {
                    Cruisers[i].transform.localPosition = shipPos1;
                    Cruisers[i].transform.Rotate(T1Rotation);
                }
                else if (Cruisers[i].tag == "Team 2") {
                    Cruisers[i].transform.localPosition = shipPos2;
                    Cruisers[i].transform.Rotate(T2Rotation);
                }

                // Check the second cruisers tag and do the same recipe
                if (Cruisers[i+1].tag == "Team 1") {
                    Cruisers[i+1].transform.localPosition = shipPos1;
                    Cruisers[i+1].transform.Rotate(T1Rotation);
                }
                else if (Cruisers[i+1].tag == "Team 2") {
                    Cruisers[i+1].transform.localPosition = shipPos2;
                    Cruisers[i+1].transform.Rotate(T2Rotation);
                }
                Cruisers[i].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
                Cruisers[i].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
                Cruisers[i+1].GetComponent<Rigidbody>().linearVelocity.Set(0,0,0);
                Cruisers[i+1].GetComponent<Rigidbody>().angularVelocity.Set(0,0,0);
            }
            else {
                generateRandPos(1, false);
                // Wake Up!
                Team1Agents[i].SetActive(true);
                Team2Agents[i].SetActive(true);
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
                Team1Agents[i].transform.Rotate(T1Rotation);
                Team2Agents[i].transform.Rotate(T2Rotation);
            }
        }
    }

    void Start()
    {
        randomShipSpawn(strikeCraftAgent, spawnStrikeShips);
        randomShipSpawn(frigateAgent, spawnFrigates);
        randomShipSpawn(cruiserAgent, spawnCruisers);
        CameraCanvas.GetComponent<CameraControl>().GetCams();
    } 
}
