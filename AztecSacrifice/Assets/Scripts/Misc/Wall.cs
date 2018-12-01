using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {
    
	void Start () {

        Transform shrine = GameObject.FindGameObjectWithTag("Shrine").transform;

        if (transform.position.x > shrine.position.x)
        {
            gameObject.tag = "WallRight";
        }
        else
        {
            gameObject.tag = "WallLeft";
        }

	}
	
}
