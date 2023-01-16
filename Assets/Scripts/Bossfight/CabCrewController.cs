using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabCrewController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Weapon weapon;
    public int stamina = 30;


    // Update is called once per frame
    void Update()
    {
        if (stamina > 5)
        {
            StartCoroutine(waitForNextBottleToFire());
        }
        else 
        {
            StartCoroutine(waitUntilStaminaRegenerates());
        }
    }

    private void FixedUpdate()
    {
        Vector2 semenPosition = GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        Vector2 aimDirection = semenPosition - rb.position;
        float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngel;
    }

    IEnumerator waitForNextBottleToFire()
    {
        yield return new WaitForSeconds(10.0f);
        stamina = stamina - 5;
        weapon.Fire();
    }

    IEnumerator waitUntilStaminaRegenerates()
    {
        yield return new WaitForSeconds(10.0f);
        stamina = 50;
    }
}
