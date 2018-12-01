using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Kid, Adult, Old }

public class AI_Stats : MonoBehaviour {

    public Phase phase = Phase.Kid;

    public float MovementSpeed = 100;
    public int MaxHealth = 3;
    public float AttackRange = 500;

    int currentHealth = 3;

    private void Start()
    {
        currentHealth = MaxHealth;
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
