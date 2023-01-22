using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Destroy(gameObject);
        //check if enemy was hit and register damage
        GameObject target = collision.collider.gameObject;
        if (target.name == "Player")
        {
            if (target.GetComponent<PlayerController>().health > 0)
            {
                target.GetComponent<PlayerController>().health -= 10;
                target.GetComponent<PlayerController>().HealthBar.SetHealth(target.GetComponent<PlayerController>().health);
            }
            else
            {
                target.GetComponent<PlayerController>().ReloadLevel();
            }
        }
    }
}
