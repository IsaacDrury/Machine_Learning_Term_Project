using System.Collections;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    //Asteroid prefabs
    [SerializeField] private GameObject asteroid1;
    [SerializeField] private GameObject asteroid2;
    [SerializeField] private GameObject asteroid3;
    [SerializeField] private GameObject asteroid4;
    [SerializeField] private GameObject asteroid5;

    //Start coordinates
    [SerializeField] private int startX;
    [SerializeField] private int startY;
    [SerializeField] private int startZ;

    //Destination coordinates
    [SerializeField] private int destX;
    [SerializeField] private int destY;
    [SerializeField] private int destZ;

    //Other values
    [SerializeField] private int speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Move()
    {
        float start = Time.deltaTime;
        Vector3 startPos = new Vector3(startX, startY, startZ);
        Vector3 destPos = new Vector3(destX, destY, destZ);
        float totalDistance = Vector3.Distance(startPos, destPos);

    }

}
