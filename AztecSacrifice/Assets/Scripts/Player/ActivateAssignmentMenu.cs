using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAssignmentMenu : MonoBehaviour {

    bool isActive = false;

    GameObject assignmentMenu;

    private void Awake()
    {
        assignmentMenu = GameObject.Find("AssignmentMenu");
    }

    private void Start()
    {
        assignmentMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("AssignmentMenu"))
        {
            isActive = !isActive;
            assignmentMenu.SetActive(isActive);
        }
    }

}
