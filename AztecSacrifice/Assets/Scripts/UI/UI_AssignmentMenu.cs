using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AssignmentMenu : MonoBehaviour {

    UnitManager um;

    public Text uKids;
    public Text uAdults;
    public Text uOld;

    public Text lKids;
    public Text lAdults;
    public Text lOld;

    public Text rKids;
    public Text rAdults;
    public Text rOld;

    public void AssignKidLeft()
    {
        um.ChangeAssignment(Side.Left, Phase.Kid);
    }

    public void AssignKidRight()
    {
        um.ChangeAssignment(Side.Right, Phase.Kid);
    }

    public void AssignAdultLeft()
    {
        um.ChangeAssignment(Side.Left, Phase.Adult);
    }

    public void AssignAdultRight()
    {
        um.ChangeAssignment(Side.Right, Phase.Adult);
    }

    public void AssignOldLeft()
    {
        um.ChangeAssignment(Side.Left, Phase.Old);
    }

    public void AssignOldRight()
    {
        um.ChangeAssignment(Side.Right, Phase.Old);
    }

    private void Awake()
    {
        um = FindObjectOfType<UnitManager>();
    }

    void UpdateVariables()
    {
        uKids.text = um.UnassignedKids.ToString();
        uAdults.text = um.UnassignedAdults.ToString();
        uOld.text = um.UnassignedOld.ToString();

        lKids.text = um.LeftKids.ToString();
        lAdults.text = um.LeftAdults.ToString();
        lOld.text = um.LeftOld.ToString();

        rKids.text = um.RightKids.ToString();
        rAdults.text = um.RightAdults.ToString();
        rOld.text = um.RightOld.ToString();
    }

    private void Update()
    {
        UpdateVariables();
    }

}
