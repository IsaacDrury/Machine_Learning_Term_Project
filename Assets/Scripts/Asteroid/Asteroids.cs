using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Asteroids : MonoBehaviour
{
    // Asteroid prefabs
    [SerializeField] private GameObject[] asteroids;
    // Transform of the asteroid field
    [SerializeField] private Transform fieldTransform;

    // Values for lerping the asteroid field
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 destPos;
    [SerializeField] private int speed;

    // Length of asteroids array
    private int length;

    // Generates a random field of astroids
    private void RandomizeField()
    {
        // Vars that will be used repeatedly in the function
        int rand = 0;
        int posX = 0;
        int posY = 0;
        int posZ = 0;
        int rotX = 0;
        int rotY = 0;
        int rotZ = 0;
        // Generates asteroids in a 3D space (Currently 32 asteroids)
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    // Generate random position
                    rand = Random.Range(10, 491);
                    posX = i * 500 + rand;
                    rand = Random.Range(10, 491);
                    posY = j * 500 + rand;
                    rand = Random.Range(10, 491);
                    posZ = k * 500 + rand;
                    Vector3 asteroidPos = new Vector3(posX, posY, posZ);

                    // Generate random asteroid
                    rand = Random.Range(0, length);
                    GameObject asteroid = Instantiate(asteroids[rand], fieldTransform);
                    asteroid.transform.localPosition = asteroidPos;

                    // Determine and apply rotation
                    rand = Random.Range(0, 6);
                    switch (rand)
                    {
                        case 0:
                            rotX = Random.Range(-60, 61);
                            rotY = 0;
                            rotZ = 0;
                            break;
                        case 1:
                            rotX = 0;
                            rotY = Random.Range(-60, 61);
                            rotZ = 0;
                            break;
                        case 2:
                            rotX = 0;
                            rotY = 0;
                            rotZ = Random.Range(-60, 61);
                            break;      
                        case 3:
                            rotX = Random.Range(-60, 61);
                            rotY = Random.Range(-60, 61);
                            rotZ = 0;
                            break;
                        case 4:
                            rotX = 0;
                            rotY = Random.Range(-60, 61);
                            rotZ = Random.Range(-60, 61);
                            break;
                        case 5:
                            rotX = Random.Range(-60, 61);
                            rotY = Random.Range(-60, 61);
                            rotZ = Random.Range(-60, 61);
                            break;
                    }
                    Vector3 asteroidRot = new Vector3(rotX, rotY, rotZ);
                    if (asteroidRot != Vector3.zero)
                    {
                        asteroid.GetComponent<AsteroidRotation>().SetRotation(asteroidRot);
                    }
                }
            }
        }
    }
    // Moves the asteroid field across the play area and then destroys it
    // Assumes that the start position, destination position, and speed are predetermined
    private IEnumerator Move()
    {
        float totalDistance = Vector3.Distance(startPos, destPos);
        float duration = totalDistance / speed;
        float time = 0.0f;
        transform.LookAt(destPos);

        while (time < (totalDistance / speed))
        {
            time += Time.deltaTime;
            fieldTransform.localPosition = Vector3.Lerp(startPos, destPos, time / duration);
            yield return null;
        }

        Destroy(this.gameObject);
    }

    // Sets initial parameters of the fields behaviour and calls other functions
    public void InitializeField(Vector3 startVector, Vector3 endVector, int fieldSpeed)
    {
        startPos = startVector;
        destPos = endVector;
        speed = fieldSpeed;
        length = asteroids.Length;
        transform.position = startPos;
        RandomizeField();
        StartCoroutine(Move());
    }
}
