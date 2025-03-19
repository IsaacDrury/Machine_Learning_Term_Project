using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    [SerializeField] private Transform asteroidTransform;

    private Vector3 asteroidRotation;

    // Update is called once per frame
    void Update()
    {
        if (asteroidRotation != Vector3.zero) 
        {
            asteroidTransform.Rotate(asteroidRotation * Time.deltaTime);
        }
    }

    public void SetRotation(Vector3 rotation)
    {
        asteroidRotation = rotation;
    }
}
