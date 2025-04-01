using Google.Protobuf.WellKnownTypes;
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
        startPos = new Vector3(-3000, -1000, 500);
        endPos = new Vector3(1000, -1000, 500);
        speed = Random.Range(10, 25);
    }

    private IEnumerator ContinuousGeneration()
    {
        InitialField();
        GenerateField();
        float time = 2000.0f / speed;
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

    private void InitialField()
    {
        GameObject field = Instantiate(asteroidField, generatorTransform);
        field.transform.localPosition = new Vector3(startPos.x + 2000, startPos.y, startPos.z);
        field.GetComponent<Asteroids>().InitializeField(field.transform.localPosition, endPos, speed);
    }

    private void ClearFields()
    {
        GameObject[] fields = GetComponentsInChildren<GameObject>();
        foreach (GameObject field in fields)
        {
            Destroy(field);
        }
    }
}
