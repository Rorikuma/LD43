using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAttackers : MonoBehaviour {

    public int EnemiesPerNight = 10;
    public int EnemiesIncrease = 1;
    public int MaxEnemiesPerNight = 40;

    public float SpawnX = 1280;

    GManager gm;

    public GameObject AttackerPrefab;

    private void Awake()
    {
        gm = GetComponent<GManager>();
    }

    public void SpawnEnemies()
    {
        //Vector2 spawnPos1 = new Vector2(SpawnX)

        //for (int i = 0; i < EnemiesPerNight; i++)
        //{
        //    if(i <= Mathf.Round(EnemiesPerNight / 2))
        //    {
        //        Instantiate(AttackerPrefab)
        //    }
        //}
    }

}
