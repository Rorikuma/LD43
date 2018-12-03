using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int MaxHealth = 20;
    public int FaithPoints = 0;
    public int Gold = 0;
    public int Food = 0;

    public int HouseCost = 10;
    public int WallCost = 10;
    public int FarmCost = 10;

    int currentHealth = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Gold")
        {
            ChangeGold();
            Destroy(collision.gameObject);
        }
    }

    private void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void ChangeGold(int g = 1)
    {
        Gold += g;
        Debug.Log(Gold);
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
        Debug.Log(Gold);
    }

    public void IncreaseFaith(int i)
    {
        FaithPoints += i;
    }

    public void ChangeFood(int f = 1)
    {
        Food += f;
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
        }
        //Debug.Log(currentHealth);
    }

    void Die()
    {
        // TODO: Player death.
    }

}
