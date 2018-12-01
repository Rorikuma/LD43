using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Defender_Attack : MonoBehaviour {

    public GameObject[] ProjectilePrefabs;

    float shootTimer = 0;

    AI_Stats stats;
    AI_Defender brain;
    Transform myTransform;

    private void Awake()
    {
        stats = GetComponent<AI_Stats>();
        brain = GetComponent<AI_Defender>();
        myTransform = this.transform;

        SimplePool.Preload(ProjectilePrefabs[0], 20);
        SimplePool.Preload(ProjectilePrefabs[1], 20);
        SimplePool.Preload(ProjectilePrefabs[2], 20);
    }

    void Attack()
    {
        GameObject g = null;
        shootTimer = 0;

        switch (stats.Age)
        {
            case Phase.Kid:
                g = SimplePool.Spawn(ProjectilePrefabs[0], myTransform.position, myTransform.rotation);
                break;

            case Phase.Adult:
                g = SimplePool.Spawn(ProjectilePrefabs[1], myTransform.position, myTransform.rotation);
                break;

            case Phase.Old:
                g = SimplePool.Spawn(ProjectilePrefabs[2], myTransform.position, myTransform.rotation);
                break;
        }

        g.GetComponent<Rigidbody2D>().AddForce(myTransform.right * stats.AttackStrength);
    }

    private void Update()
    {
        if(brain.State == AIState.Attacking && shootTimer > stats.Firerate)
        {
            Attack();
        }

        shootTimer += Time.deltaTime;
    }

}
