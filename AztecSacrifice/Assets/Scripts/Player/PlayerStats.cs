using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int MaxHealth = 20;

    int currentHealth = 1;

    private void Awake()
    {
        currentHealth = MaxHealth;
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
