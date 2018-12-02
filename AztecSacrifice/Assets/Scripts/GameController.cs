using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float DayTime = 240;
    public float MaxAlpha = 0.5f;

    int day = 1;
    int halfDay = 0;
    float time = 0;

    public SpriteRenderer NightSprite;

    void NewDay()
    {
        SpawnDefenders[] houses = FindObjectsOfType<SpawnDefenders>();

        foreach(SpawnDefenders h in houses)
        {
            h.NewDay();
        }

        GameObject[] defenders = GameObject.FindGameObjectsWithTag("Defender");

        foreach(GameObject g in defenders)
        {
            g.GetComponent<AI_Stats>().IncreaseAge();
        }
    }

    private void Start()
    {
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
                day += 1;
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
