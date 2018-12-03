using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {

    public int MaxHealth = 20;
    public int FaithPoints = 0;
    public int Gold = 0;
    public int Food = 0;

    public int HouseCost = 10;
    public int WallCost = 10;
    public int FarmCost = 10;

    int currentHealth = 1;

    float goldChangeTimer = 0;

    UI_Resources resources;
    UnitManager um;

    public TextMesh HealthText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gold" && Time.time > goldChangeTimer)
        {
            goldChangeTimer = Time.time + 0.1f;
            ChangeGold();
            Destroy(collision.gameObject);
        }
    }

    private void Awake()
    {
        um = FindObjectOfType<UnitManager>();
        currentHealth = MaxHealth;
        resources = FindObjectOfType<UI_Resources>();
        resources.UpdateFaith(FaithPoints);
        resources.UpdateFood(Food);
        resources.UpdateGold(Gold);
    }

    private void Start()
    {
        UpdateHealthText();
    }

    public void ChangeGold(int g = 1)
    {
        Gold += g;
        resources.UpdateGold(Gold);
    }

    public void ChangeGold(BuildingType type, int i)
    {
        switch (type)
        {
            case BuildingType.House:
                Gold += HouseCost * i;
                break;
            case BuildingType.Wall:
                Gold += WallCost * i;
                break;
            case BuildingType.Farm:
                Gold += FarmCost * i;
                break;
        }
        resources.UpdateGold(Gold);
    }

    public void IncreaseFaith(int i)
    {
        FaithPoints += i;
        resources.UpdateFaith(FaithPoints);
    }

    public void ChangeFood(int f = 1)
    {
        Food += f;

        if(Food < 0)
        {
            um.NotEnoughFood(Food);
            Food = 0;
        }

        resources.UpdateFood(Food);
    }

    void UpdateHealthText()
    {
        HealthText.text = currentHealth.ToString();
    }

    public void TakeDamage(int dmg)
    {
        if(currentHealth - dmg > 0)
        {
            currentHealth -= dmg;
        }
        else
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthText();
    }

    public void Heal()
    {
        currentHealth = MaxHealth;

        UpdateHealthText();
    }

    void Die()
    {
        // TODO: Player death.

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
