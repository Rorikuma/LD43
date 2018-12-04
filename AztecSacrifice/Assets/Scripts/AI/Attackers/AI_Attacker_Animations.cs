using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Attacker_Animations : MonoBehaviour {

    public GameObject[] NormalEnemyAnimations;
    public GameObject[] GhostAnimations;

    bool isAGhost = false;
    int index = 0;

    AI_Stats stats;

    private void Awake()
    {
        stats = GetComponent<AI_Stats>();

        isAGhost = stats.IsAGhost;

        if (isAGhost)
        {
            index = Random.Range(0, GhostAnimations.Length);
            GhostAnimations[index].SetActive(true);
        }
        else
        {
            index = Random.Range(0, NormalEnemyAnimations.Length);
            NormalEnemyAnimations[index].SetActive(true);
        }
    }
    
}
