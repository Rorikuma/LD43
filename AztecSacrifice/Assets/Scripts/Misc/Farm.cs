using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : MonoBehaviour {

    public int FoodIncrease = 1;

    int spawnedDay = 0;

    bool hasFood = false;
    bool collidingWithPlayer = false;

    PlayerStats pStats;
    GManager gm;

    public Sprite Notification;
    public SpriteRenderer NotificationRenderer;

    private void Start()
    {
        gm = FindObjectOfType<GManager>();
        pStats = FindObjectOfType<PlayerStats>();

        FindObjectOfType<UnitManager>().RegisterBuilding(this.transform);

        spawnedDay = gm.Day;
    }

    public void NewDay()
    {
        if(gm.Day == spawnedDay + 3)
        {
            hasFood = true;
            NotificationRenderer.sprite = Notification;
        }
    }

    void IncreaseFood()
    {
        spawnedDay = gm.Day;
        hasFood = false;
        NotificationRenderer.sprite = null;
        pStats.ChangeFood(FoodIncrease);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Grab") && collidingWithPlayer && hasFood)
        {
            IncreaseFood();
        }
    }
}
