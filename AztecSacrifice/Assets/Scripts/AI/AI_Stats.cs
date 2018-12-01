using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Kid, Adult, Old }

public class AI_Stats : MonoBehaviour {

    public Phase Age = Phase.Kid;

    public float MovementSpeed = 100;
    public int MaxHealth = 5;
    public float AttackRange = 500;
    public float AttackStrength = 1000;
    public float Firerate = 0.5f;

    int kidHealth = 5;
    int adultHealth = 10;
    int oldHealth = 8;
    float kidFirerate = 0.5f;
    float adultFirerate = 0.5f;
    float oldFirerate = 0.5f;

    int currentHealth = 5;

    private void Start()
    {
        MaxHealth = kidHealth;
        currentHealth = MaxHealth;
        Firerate = kidFirerate;
    }

    public void UpgradeUnit()
    {
        if(Age == Phase.Kid)
        {
            Age = Phase.Adult;
            MaxHealth = adultHealth;
            currentHealth = adultHealth;
            Firerate = adultFirerate;
        }
        if(Age == Phase.Adult)
        {
            Age = Phase.Old;
            MaxHealth = oldHealth;
            currentHealth = oldHealth;
            Firerate = oldFirerate;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int dmg)
    {
        if(currentHealth - dmg <= 0)
        {
            currentHealth = 0;
            Die();
        }
        else
        {
            currentHealth -= dmg;
        }
    }

}
