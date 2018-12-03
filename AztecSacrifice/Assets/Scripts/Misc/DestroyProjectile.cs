using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<AI_Stats>().TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "Floor")
        {
            Destroy(gameObject);
        }
    }

}
