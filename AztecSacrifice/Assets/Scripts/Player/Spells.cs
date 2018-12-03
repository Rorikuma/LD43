using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour {

    public int FireballCost = 1;
    public float FireballSpeed = 10000;

    public int MagicWallCost = 20;

    public int HealingCost = 30;

    public Transform Barrel;
    public GameObject FireballPrefab;
    public GameObject MagicWallPrefab;
    public GameObject HealingPrefab;

    Transform t;
    PlayerStats stats;

    void ShootFireball()
    {
        GameObject g = SimplePool.Spawn(FireballPrefab, Barrel.position, Barrel.rotation);
        g.GetComponent<Rigidbody2D>().velocity = Barrel.right * FireballSpeed;
        stats.IncreaseFaith(-FireballCost);
    }

    void MagicWall()
    {
        Instantiate(MagicWallPrefab, t.position + t.right * 16, t.rotation);
        stats.IncreaseFaith(-MagicWallCost);
    }

    void Heal()
    {
        Instantiate(HealingPrefab, t.position, t.rotation);
        stats.Heal();
        stats.IncreaseFaith(-HealingCost);
    }

    private void Awake()
    {
        t = this.transform;
        stats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        SimplePool.Preload(FireballPrefab, 20);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Spell1") && stats.FaithPoints >= FireballCost)
        {
            ShootFireball();
        }

        if(Input.GetButtonDown("Spell2") && stats.FaithPoints >= MagicWallCost)
        {
            MagicWall();
        }

        if (Input.GetButtonDown("Spell3") && stats.FaithPoints >= HealingCost)
        {
            Heal();
        }
    }

}
