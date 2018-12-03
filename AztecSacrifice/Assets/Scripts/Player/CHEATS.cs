using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHEATS : MonoBehaviour {

    bool cheatMode = false;

    private void Start()
    {
        if (Application.isEditor)
        {
            cheatMode = true;
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.BackQuote))
        {
            cheatMode = !cheatMode;
        }

        if (cheatMode)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                GetComponent<PlayerStats>().ChangeGold(100);
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                GetComponent<PlayerStats>().IncreaseFaith(100);
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                GetComponent<PlayerStats>().ChangeFood(100);
            }
        }
    }

}
