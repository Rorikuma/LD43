using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDefenders : MonoBehaviour {

    public int KidPoints = 2;
    public int AdultPoints = 4;
    public int OldPoints = 1;

    public GameObject GrabPosition;
    public Sprite[] Sprites;

    [HideInInspector]
    public bool isCollidingWithDefender = false;
    [HideInInspector]
    public bool isShrineColliding = false;
    [HideInInspector]
    public GameObject Defender;
    [HideInInspector]
    public bool isHolding = false;

    PlayerStats stats;
    UnitManager um;
    GManager gm;

    Phase grabbedPhase;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Shrine")
        {
            isShrineColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shrine")
        {
            isShrineColliding = false;
        }
    }

    void GrabDefender()
    {
        isHolding = true;
        
        switch (grabbedPhase)
        {
            case Phase.Kid:
                GrabPosition.GetComponent<SpriteRenderer>().sprite = Sprites[0];
                break;

            case Phase.Adult:
                GrabPosition.GetComponent<SpriteRenderer>().sprite = Sprites[1];
                break;

            case Phase.Old:
                GrabPosition.GetComponent<SpriteRenderer>().sprite = Sprites[2];
                break;
        }

        um.DeregisterDefender(Defender.GetComponent<AI_Defender>());
        Destroy(Defender);
        Defender = null;
    }

    void Sacrifice()
    {
        isHolding = false;
        
        switch (grabbedPhase)
        {
            case Phase.Kid:
                stats.IncreaseFaith(KidPoints);
                GrabPosition.GetComponent<SpriteRenderer>().sprite = null;
                break;

            case Phase.Adult:
                stats.IncreaseFaith(AdultPoints);
                GrabPosition.GetComponent<SpriteRenderer>().sprite = null;
                break;

            case Phase.Old:
                stats.IncreaseFaith(OldPoints);
                GrabPosition.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }

        gm.SacrificedToday();
    }

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        um = FindObjectOfType<UnitManager>();
        gm = FindObjectOfType<GManager>();
    }

    private void Update()
    {
        if(isCollidingWithDefender && Input.GetButtonDown("Grab") && isHolding == false)
        {
            grabbedPhase = Defender.GetComponent<AI_Stats>().Age;

            GrabDefender();
        }

        if (isShrineColliding && isHolding)
        {
            Sacrifice();
        }
    }

}
