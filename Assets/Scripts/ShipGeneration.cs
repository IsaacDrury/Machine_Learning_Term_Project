using System.Xml.Serialization;
using UnityEngine;

public class ShipGeneration : MonoBehaviour
{

    [SerializeField] GameObject[] strikecraftAgents;
    [SerializeField] GameObject strikecraftAgent;

    [SerializeField] int spawnStrikecraft;

    private Vector3 shipPosition = Vector3.zero;
    private Vector3 shipRotation = Vector3.zero;

    private void randomShipSpawn() {

        for (int i = 0; i < spawnStrikecraft; i++) {

            generateRandPosRot();

            //Instantiate it
            GameObject agent = Instantiate(strikecraftAgent);

            //Position it
            agent.transform.localPosition = shipPosition;
            
            //Rotate it
            if (shipRotation != Vector3.zero)
            {
                agent.GetComponent<AsteroidRotation>().SetRotation(shipRotation);
            }

            //Save it for later
            strikecraftAgents[i] = strikecraftAgent;
        }

    }

    private void generateRandPosRot() {
        int posX = Random.Range(-500, 500);
        int posY = Random.Range(-500, 500);
        int posZ = Random.Range(-500, 500);
        shipPosition = new Vector3(posX, posY, posZ);
        int rotX = Random.Range(-90,90);
        int rotY = Random.Range(-90,90);
        int rotZ = Random.Range(-90,90);
        shipRotation = new Vector3(rotX, rotY, rotZ);

    }
    public void randomShipReset() {
        foreach (GameObject strikecraft in strikecraftAgents)
        {
            generateRandPosRot();

            strikecraft.transform.localPosition = shipPosition;
            
            if (shipRotation != Vector3.zero)
            {
                strikecraft.transform.Rotate(shipRotation);
            }
        }
    }

    void Start()
    {
        randomShipSpawn();
    } 
}
