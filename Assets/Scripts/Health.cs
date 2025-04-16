using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject ShipGenerator;
    [SerializeField] private int maxHealth;
    [SerializeField] private string type;

    private ShipGeneration generatorScript;
    private Slider slider;
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generatorScript = ShipGenerator.GetComponent<ShipGeneration>();
        
        health = maxHealth;
        slider = this.GetComponent<Slider>();
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    // Updates health bar and "kills" the ship
    public void ChangeHealth(int damage)
    {
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            this.gameObject.transform.parent.gameObject.SetActive(false);
            generatorScript.checkReset();
        }
    }

    // Reset health and health bar
    public void ResetHealth()
    {
        health = maxHealth;
        slider.value = health;
    }

    // Returns ship type (Strike-craft, Frigate, Cruiser)
    public string GetShipType()
    {
        return type;
    }
}
