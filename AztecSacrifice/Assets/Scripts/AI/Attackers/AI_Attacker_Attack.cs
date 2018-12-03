using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attacker_Attack : MonoBehaviour {

    float attackTimer = 0;

    AI_Attacker brain;
    AI_Stats stats;

    void Attack()
    {
        attackTimer = Time.time + stats.Firerate;
        if (brain.target.root.tag == "Player")
        {
            brain.target.GetComponent<PlayerStats>().TakeDamage(stats.Damage);
        }
        else
        {
            brain.target.GetComponent<AI_Stats>().TakeDamage(stats.Damage);
        }
    }

    private void Awake()
    {
        stats = GetComponent<AI_Stats>();
        brain = GetComponent<AI_Attacker>();
    }

    private void Update()
    {
        if(brain.target != null && brain.State == AIState.Attacking && Time.time > attackTimer)
        {
            Attack();
        }
    }

}
