using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour {

    GrabDefenders grab;

    GameObject defender;

    private void Awake()
    {
        grab = transform.root.gameObject.GetComponent<GrabDefenders>();
    }

    public void ResetDefender()
    {
        defender = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Defender")
        {
            grab.isCollidingWithDefender = true;
            defender = collision.gameObject;
            grab.Defender = defender;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Defender")
        {
            grab.isCollidingWithDefender = false;
            defender = null;
            grab.Defender = null;
        }
    }

}
