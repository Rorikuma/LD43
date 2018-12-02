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

            switch(Random.Range(0, 3))
            {
                case 0:
                    g.GetComponent<AI_Stats>().Age = Phase.Kid;
                    break;

                case 1:
                    g.GetComponent<AI_Stats>().Age = Phase.Adult;
                    break;

                case 2:
                    g.GetComponent<AI_Stats>().Age = Phase.Old;
                    break;
            }
            
            g.GetComponent<AI_Defender>().ChangeAssignment(lastSide);
        }
    }

}
