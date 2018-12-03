using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Resources : MonoBehaviour {

    public Text Gold;
    public Text Faith;
    public Text Food;
    
    public void UpdateGold(int i)
    {
        Gold.text = "Gold: " + i.ToString();
    }

    public void UpdateFood(int i)
    {
        Food.text = "Food: " + i.ToString();
    }

    public void UpdateFaith(int i)
    {
        Faith.text = "Faith: " + i.ToString();
    }

}
