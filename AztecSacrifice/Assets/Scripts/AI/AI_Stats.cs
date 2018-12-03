using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Kid, Adult, Old }

public class AI_Stats : MonoBehaviour
{
    public bool NeverGetOld = false;
    public bool IsABuilding = false;

    public Phase Age = Phase.Kid;

    public float MovementSpeed = 100;
    public int MaxHealth = 5;
    public float AttackRange = 500;
    public float AttackStrength = 1000;
    public float Firerate = 0.5f;
    public int Damage = 1;

    public int KidHealth = 5;
    public int AdultHealth = 10;
    public int OldHealth = 8;

    public float KidFirerate = 2f;
    public float AdultFirerate = 1f;
    public float OldFirerate = 1.5f;

    public int GhostHealth = 15;
    public bool IsAGhost = false;

    public GameObject CoinPrefab;

    public int currentHealth = 5;

    Transform myTransform;

    UnitManager um;

    public TextMesh HealthText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Healing")
        {
            currentHealth = MaxHealth;
        }
    }

    private void Start()
    {
        myTransform = this.transform;
        if(gameObject.tag == "Defender")
        {
            switch (Age)
            {
                case Phase.Kid:
                    MaxHealth = KidHealth;
                    break;
                case Phase.Adult:
                    MaxHealth = AdultHealth;
                    break;
                case Phase.Old:
                    MaxHealth = OldHealth;
                    break;
            }
        }
        else if (IsAGhost)
        {
            MaxHealth = GhostHealth;
        }

        currentHealth = MaxHealth;

        UpdateHealthText();
    }

    private void Awake()
    {
        um = FindObjectOfType<UnitManager>();
    }

    public void IncreaseAge()
    {
        if (NeverGetOld == false)
        {
            um.DeregisterDefender(GetComponent<AI_Defender>());

            if (Age == Phase.Kid)
            {
                Age = Phase.Adult;
                MaxHealth = AdultHealth;
                currentHealth = AdultHealth;
                Firerate = AdultFirerate;
            }
            else if (Age == Phase.Adult)
            {
                Age = Phase.Old;
                MaxHealth = OldHealth;
                currentHealth = OldHealth;
                Firerate = OldFirerate;
            }
            else if (Age == Phase.Old)
            {
                // TODO: Uncomment when making a final build
                //Die();
            }

            UpdateHealthText();
            um.RegisterDefender(GetComponent<AI_Defender>());
        }
    }

    void UpdateHealthText()
    {
        if (HealthText != null)
        {
            HealthText.text = currentHealth.ToString();
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

        if (IsABuilding)
        {
            Debug.Log("test");
            um.DeregisterBuilding(myTransform);
            Destroy(myTransform.root);
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

        UpdateHealthText();
    }

    public void Heal(int h)
    {
        if(currentHealth + h > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        UpdateHealthText();
    }

}
