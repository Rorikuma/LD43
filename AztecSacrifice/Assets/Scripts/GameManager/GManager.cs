using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{

    public float DayTime = 240;
    public float MaxAlpha = 0.5f;

    public int Day = 1;
    int halfDay = 0;
    float time = 0;

    public SpriteRenderer NightSprite;

    SpawnAttackers sa;

    void NewDay()
    {
        SpawnDefenders[] houses = FindObjectsOfType<SpawnDefenders>();

        foreach (SpawnDefenders h in houses)
        {
            h.NewDay();
        }

        GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");

        foreach (GameObject g in defenders)
        {
            g.GetComponent<AI_Stats>().IncreaseAge();
        }

        GameObject[] farms = GameObject.FindGameObjectsWithTag("Farm");
        
        foreach (GameObject g in farms)
        {
            g.GetComponent<Farm>().NewDay();
        }
    }

    void Night()
    {
        sa.SpawnEnemies();
    }

    private void Start()
    {
        sa = GetComponent<SpawnAttackers>();
        NightSprite.color = new Color(NightSprite.color.r, NightSprite.color.g, NightSprite.color.b, 0);
    }

    void ControlDayAndNight()
    {
        if (halfDay % 2 == 0)
        {
            time += Time.deltaTime;

            if (time >= DayTime)
            {
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
