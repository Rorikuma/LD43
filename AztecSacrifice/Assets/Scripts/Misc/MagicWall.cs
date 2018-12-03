using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWall : MonoBehaviour {

    public float Lifetime = 5f;
    
	void Start () {
        Destroy(gameObject, Lifetime);
	}

}
