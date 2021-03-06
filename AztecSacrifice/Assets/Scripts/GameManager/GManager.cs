﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public float DayTime = 240;
    public float MaxAlpha = 0.5f;

    public int Day = 1;
    int halfDay = 0;
    float time = 0;

    bool sacrificed = false;
    bool nextSpawnGhosts = false;

    public SpriteRenderer NightSprite;

    public GameObject MusicPlayerPrefab;
    public GameObject SoundsPlayerPrefab;

    SpawnAttackers sa;
    PlayerStats ps;

    public void SacrificedToday()
    {
        sacrificed = true;
    }

    void NewDay()
    {
        if (sacrificed)
        {
            sacrificed = false;
        }
        else
        {
            nextSpawnGhosts = true;
        }

        SpawnDefenders[] houses = FindObjectsOfType<SpawnDefenders>();

        foreach (SpawnDefenders h in houses)
        {
            h.NewDay();
        }

        GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");

        int i = 0;

        foreach (GameObject g in defenders)
        {
            g.GetComponent<AI_Stats>().IncreaseAge();
            i++;
        }

        ps.ChangeFood(-i);

        GameObject[] farms = GameObject.FindGameObjectsWithTag("Farm");
        
        foreach (GameObject g in farms)
        {
            g.GetComponent<Farm>().NewDay();
        }
    }

    void Night()
    {
        sa.SpawnEnemies(nextSpawnGhosts);
        nextSpawnGhosts = false;
    }

    private void Start()
    {
        ps = FindObjectOfType<PlayerStats>();
        sa = GetComponent<SpawnAttackers>();
        NightSprite.color = new Color(NightSprite.color.r, NightSprite.color.g, NightSprite.color.b, 0);

        if(FindObjectOfType<Music>() == null)
        {
            Instantiate(MusicPlayerPrefab, Vector3.zero, Quaternion.identity);
        }

        Instantiate(SoundsPlayerPrefab, Vector3.zero, Quaternion.identity);
    }

    void ControlDayAndNight()
    {
        if (halfDay % 2 == 0)
        {
            time += Time.deltaTime;

            if (time >= DayTime)
            {
                Night();
                time = DayTime;
                halfDay += 1;
            }
        }
        else
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                // New Day

                NewDay();
                time = 0;
                halfDay += 1;
                Day += 1;
            }
        }

        float alpha = (MaxAlpha * time) / DayTime;
        NightSprite.color = new Color(NightSprite.color.r, NightSprite.color.g, NightSprite.color.b, alpha);
    }

    private void Update()
    {
        ControlDayAndNight();
    }

}
