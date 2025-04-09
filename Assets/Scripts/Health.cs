using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private int maxHealth;

    private Slider slider;
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        slider = healthBar.GetComponent<Slider>();
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    // Updates health bar and "kills" the ship
    public void ChangeHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
        slider.value = health;
    }

    // Reset health and health bar
    public void ResetHealth()
    {
        health = maxHealth;
    }
}
