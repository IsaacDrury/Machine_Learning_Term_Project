using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Unity.MLAgents;
using UnityEngine;

public class ShipGeneration : MonoBehaviour
{
    // Ships generated
    [SerializeField] private List<GameObject> Team1Agents;
    [SerializeField] private List<GameObject> Team2Agents;
    [SerializeField] private List<GameObject> Cruisers;

    // Ship prefabs
    [SerializeField] private GameObject strikeCraftAgent;
    [SerializeField] private GameObject frigateAgent;
    [SerializeField] private GameObject cruiserAgent;

    // Team specific values
    [SerializeField] private Material team1Material;
    [SerializeField] private Material team2Material;
    [SerializeField] private Material trailMaterial;
    [SerializeField] private Vector3 T1Rotation = Vector3.zero;
    [SerializeField] private Vector3 T2Rotation = Vector3.zero;

    [SerializeField] private Canvas CameraCanvas;

    // Ship amounts
    public int numStrikeShips;
    public int numFrigates;
    public int numCruisers;

    private Vector3 shipPos1 = Vector3.zero;
    private Vector3 shipPos2 = Vector3.zero;
    private bool isCruiser = false;

    public int checkReset() {
        int numTeam1Agents = Team1Agents.Count;
        int numTeam2Agents = Team2Agents.Count;
        int numCruiserAgents = Cruisers.Count;
        bool allDead = true;
        
        foreach (GameObject ship in Team1Agents) {
            if (ship.activeSelf) {
                allDead = false;
                break;
            }
        }

        if (allDead && Cruisers[0].activeSelf == false) {
            // I'm sorry...how many ships, sir?
            Debug.Log("T1: " + numTeam1Agents + " T2: " + numTeam2Agents + " Cruisers: " + numCruiserAgents);
            // The above list sizes be the same numbers as the integers below
            Debug.Log("S: " + numStrikeShips + " F: " + numFrigates + " C: " + numCruisers);
            randomShipReset(numTeam1Agents, false);
            randomShipReset(numCruisers, true);
            StartCoroutine(TimeDelay(5.0f));
            return 0;
        }

        //else reset to true to check team 2
        allDead = true;
        foreach (GameObject ship in Team2Agents) {
            if (ship.activeSelf) {
                allDead = false;
                break;
            }
        }

        if (allDead && Cruisers[0].activeSelf == false) {
            // I'm sorry...how many ships, sir?
            Debug.Log("T1: " + numTeam1Agents + " T2: " + numTeam2Agents + " Cruisers: " + numCruiserAgents);
            // The above list sizes be the same numbers as the integers below
            Debug.Log("S: " + numStrikeShips + " F: " + numFrigates + " C: " + numCruisers);
            randomShipReset(numTeam1Agents, false);
            randomShipReset(numCruisers, true);
            StartCoroutine(TimeDelay(5.0f));
            return 0;
        }
        return 0;
    }

    IEnumerator TimeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log( delay + " seconds.");
    }

    private void randomShipSpawn(GameObject shipAgent, int numShips) {
        Debug.Log("Spawning for da first time!");

        for (int i = 0; i < numShips; i++)
        {
            if (shipAgent.gameObject.transform.GetChild(1).GetComponent<Health>().GetShipType() == "Cruiser") {
                isCruiser = true;
                generateRandPos(1, isCruiser);
            }
            else {
                generateRandPos(1, false);
            }

            // Instantiate agent on team 1
            GameObject agent1 = Instantiate(shipAgent);
            agent1.tag = "Team 1";
            Renderer[] renderers = agent1.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers) 
            {
                renderer.material = team1Material;

            }
            // Position them
            agent1.transform.position = shipPos1;
            // Rotato the potatoes
            agent1.transform.Rotate(T1Rotation);
            // Setting trail gradient color for Team2 which fades from front-to-back
            agent1.GetComponent<TrailRenderer>().material = trailMaterial;
            agent1.GetComponent<TrailRenderer>().startColor = new Color(1.0f, 0.4f, 0.0f);
            agent1.GetComponent<TrailRenderer>().endColor = Color.white;

            // Instantiate agent on team 2
            GameObject agent2 = Instantiate(shipAgent);
            agent2.tag = "Team 2";
            renderers = agent2.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material = team2Material;
            }
            // Position and rotate agent
            agent2.transform.position = shipPos2;
            agent2.transform.Rotate(T2Rotation);
            agent2.GetComponent<TrailRenderer>().material = trailMaterial;

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
        Debug.Log("Generating two random positions");
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
        Debug.Log("Resetting ships");
        
        for (int i = 0; i < numShips; i++)
        {   
            // Where we at?
            Debug.Log("Index: " + i + " " + Team1Agents.Count + " " + Team2Agents.Count + " " + Cruisers.Count);

            if (Cruiser) {
                generateRandPos(1, true);
                Cruisers[i].SetActive(true);
                // Check the first cruisers tag and reset team-specific variables
                if (Cruisers[i].tag == "Team 1") {
                    Cruisers[i].transform.position = shipPos1;
                    Cruisers[i].transform.Rotate(T1Rotation);
                }
                else if (Cruisers[i].tag == "Team 2") {
                    Cruisers[i].transform.position = shipPos2;
                    Cruisers[i].transform.Rotate(T2Rotation);
                }

                // Check the second cruisers tag and do the same recipe
                if (Cruisers[i+1].tag == "Team 1") {
                    Cruisers[i+1].transform.position = shipPos1;
                    Cruisers[i+1].transform.Rotate(T1Rotation);
                }
                else if (Cruisers[i+1].tag == "Team 2") {
                    Cruisers[i+1].transform.position = shipPos2;
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
                Team1Agents[i].transform.position = shipPos1;
                Team2Agents[i].transform.position = shipPos2;
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
        Team1Agents = new List<GameObject>();
        Team2Agents = new List<GameObject>();
        Cruisers = new List<GameObject>();
        randomShipSpawn(strikeCraftAgent, numStrikeShips);
        randomShipSpawn(frigateAgent, numFrigates);
        randomShipSpawn(cruiserAgent, numCruisers);
        CameraCanvas.GetComponent<CameraControl>().GetCams();
    }
}
