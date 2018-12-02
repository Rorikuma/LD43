using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDefenders : MonoBehaviour {

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

        // TODO: Add sprites.

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

        Destroy(Defender);
        Defender = null;
    }

    void Sacrifice()
    {
        isHolding = false;

        Debug.Log("Sacrifice" + grabbedPhase);

        // TODO: Add sacrifice points.

        switch (grabbedPhase)
        {
            case Phase.Kid:
                GrabPosition.GetComponent<SpriteRenderer>().sprite = null;
                break;

            case Phase.Adult:
                GrabPosition.GetComponent<SpriteRenderer>().sprite = null;
                break;

            case Phase.Old:
                GrabPosition.GetComponent<SpriteRenderer>().sprite = null;
                break;
        }
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
