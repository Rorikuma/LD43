using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Kid, Adult, Old }

public class AI_Stats : MonoBehaviour
{
    public bool NeverGetOld = false;

    public Phase Age = Phase.Kid;

    public float MovementSpeed = 100;
    public int MaxHealth = 5;
    public float AttackRange = 500;
    public float AttackStrength = 1000;
    public float Firerate = 0.5f;
    public int damage = 1;

    int kidHealth = 5;
    int adultHealth = 10;
    int oldHealth = 8;

    public float KidFirerate = 2f;
    public float AdultFirerate = 1f;
    public float OldFirerate = 1.5f;

    public GameObject CoinPrefab;

    int currentHealth = 5;

    Transform myTransform;

    UnitManager um;

    private void Start()
    {
        um = FindObjectOfType<UnitManager>();
        myTransform = this.transform;
        MaxHealth = kidHealth;
        currentHealth = MaxHealth;
    }

    public void IncreaseAge()
    {
        if (NeverGetOld == false)
        {
            um.DeregisterDefender(GetComponent<AI_Defender>());

            if (Age == Phase.Kid)
            {
                Age = Phase.Adult;
                MaxHealth = adultHealth;
                currentHealth = adultHealth;
                Firerate = AdultFirerate;
            }
            else if (Age == Phase.Adult)
            {
                Age = Phase.Old;
                MaxHealth = oldHealth;
                currentHealth = oldHealth;
                Firerate = OldFirerate;
            }
            else if (Age == Phase.Old)
            {
                // TODO: Uncomment when making a final build
                //Die();
            }

            um.RegisterDefender(GetComponent<AI_Defender>());
        }
    }

    void Die()
    {
        if (gameObject.tag == "Enemy")
        {
            DropGold();
        }
        else if (gameObject.tag == "Defender")
        {
            um.DeregisterDefender(GetComponent<AI_Defender>());
        }

        Destroy(gameObject);
    }

    void DropGold(int g = 1)
    {
        SimplePool.Spawn(CoinPrefab, myTransform.position, Quaternion.identity);
    }

    public void TakeDamage(int dmg = 1)
    {
        if (currentHealth - dmg <= 0)
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
