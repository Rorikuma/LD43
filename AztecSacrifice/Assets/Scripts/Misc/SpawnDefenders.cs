using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDefenders : MonoBehaviour {

    public float ChanceToSpawn = 0.75f;

    public GameObject DefenderPrefab;

    public SpriteRenderer SR;
    public Sprite Notification;

    bool hasKid = false;
    bool collidingPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collidingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidingPlayer = false;
        }
    }

    void SpawnKid()
    {
        GameObject g = SimplePool.Spawn(DefenderPrefab, transform.position, Quaternion.identity);
        hasKid = false;

        SR.sprite = null;
    }

	public void NewDay()
    {
        float rand = Random.Range(0f, 1f);

        if(rand <= ChanceToSpawn)
        {
            hasKid = true;

            SR.sprite = Notification;
        }
    }

    private void Start()
    {
        FindObjectOfType<UnitManager>().RegisterBuilding(this.transform);
    }

    private void Update()
    {
        if(collidingPlayer && Input.GetButtonDown("Grab") && hasKid)
        {
            SpawnKid();
        }
    }

}
