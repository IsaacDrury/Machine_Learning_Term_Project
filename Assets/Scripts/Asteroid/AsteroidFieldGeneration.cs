using System.Collections;
using UnityEngine;

public class AsteroidFieldGeneration : MonoBehaviour
{
    [SerializeField] GameObject asteroidField;
    [SerializeField] Transform generatorTransform;
    [SerializeField] Vector3 startPos;
    [SerializeField] Vector3 endPos;
    [SerializeField] int speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateFieldParams();
        StartCoroutine("ContinuousGeneration");
    }

    public void GenerateFieldParams()
    {
        int dir = Random.Range(0, 4);
        int x = 0;
        int y = -500;
        int z = 0;
        switch (dir)
        {
            case 0:
                z = Random.Range(250, 750);
                y = Random.Range(-750, -250);
                startPos = new Vector3(500, y, 500 - (z - 500));
                y = y + 500;
                endPos = new Vector3(-1500, -500 - y, 500 - (z - 500));
                break;
            case 1:
                z = Random.Range(250, 750);
                y = Random.Range(-750, -250);
                startPos = new Vector3(-1500, y, z);
                y = y + 500;
                endPos = new Vector3(500, -500 - y, z);
                break;
            case 2:
                x = Random.Range(250, 750);
                y = Random.Range(-750, -250);
                startPos = new Vector3(x, y, 500);
                y = y + 500;
                endPos = new Vector3(x, -500 - y, -1500);
                break;
            case 3:
                x = Random.Range(250, 750);
                y = Random.Range(-750, -250);
                startPos = new Vector3(500 - (x - 500), y, -1500);
                y = y + 500;
                endPos = new Vector3(500 - (x - 500), - 500 - y, 500);
                break;
        }
        speed = Random.Range(10, 50);
    }

    private IEnumerator ContinuousGeneration()
    {
        GenerateField();
        float time = 1000.0f / speed;
        while (true) 
        { 
            yield return new WaitForSeconds(time);
            GenerateField();
        }
    }

    private void GenerateField()
    {
        GameObject field = Instantiate(asteroidField, generatorTransform);
        field.transform.localPosition = startPos;
        field.GetComponent<Asteroids>().InitializeField(startPos, endPos, speed);
    }
}
