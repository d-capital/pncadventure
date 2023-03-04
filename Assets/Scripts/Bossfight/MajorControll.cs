using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorControll : MonoBehaviour
{
    public Rigidbody2D rb;
    public Weapon weapon;
    public int stamina = 30;
    public int health = 100;
    public float fireRate;
    private float nextFire;
    public float coolDownRate;

    public string Name = "Ìýð";

    public HealthBarControll HealthBar;

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>().isCabCrewDead == true 
            && GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>().isQteActive != true)
        {
            if (stamina > 5 && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                fireNextBottle();
            }
            else
            {
                if (Time.time > coolDownRate)
                {
                    stamina = 30;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 semenPosition = GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        Vector3 aimDirection = semenPosition - transform.position;
        float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngel;
    }

    void fireNextBottle()
    {

        stamina = stamina - 5;
        weapon.Fire(stamina);

    }

}
