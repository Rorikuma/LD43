using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_SpawnDefender : MonoBehaviour {

    Side lastSide = Side.Right;

    public GameObject DefenderPrefab;

    private void Awake()
    {
        if(Application.isEditor == false)
        {
            GetComponent<Test_SpawnDefender>().enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameObject g = Instantiate(DefenderPrefab, transform.position, Quaternion.identity);
            if (lastSide == Side.Right)
            {
                lastSide = Side.Left;
            }
            else
            {
                lastSide = Side.Right;
            }

            g.GetComponent<AI_Defender>().ChangeAssignment(lastSide);
        }
    }

}
