using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTextRotation : MonoBehaviour {

    Transform t;

    private void Awake()
    {
        t = this.transform;
    }

    void LateUpdate () {
        t.rotation = Quaternion.identity;
	}
}
