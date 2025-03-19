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
                x = 1000;
                z = Random.Range(-500, 500);
                break;
            case 1:
                x = -1000;
                z = Random.Range(-500, 500);
                break;
            case 2:
                z = 1000;
                x = Random.Range(-500, 500);
                break;
            case 3:
                z = -1000;
                x = Random.Range(-500, 500);
                break;
                y = Random.Range(-750, -250);
        }
        startPos = new Vector3(x, y, z);
        y = y + 500;
        endPos = new Vector3(-x, 500 - y, -z);
        speed = Random.Range(5, 30);
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
