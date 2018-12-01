using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Stats : MonoBehaviour {

    public float MovementSpeed = 100;
    public int MaxHealth = 3;

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
