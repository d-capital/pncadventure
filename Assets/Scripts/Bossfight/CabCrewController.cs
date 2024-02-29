using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabCrewController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Weapon weapon;
    public int stamina = 30;
    public int health = 60;
    public float fireRate;
    private float nextFire;
    public float coolDownRate;
    public bool hasToRun = false;
    public bool isDestinationSet = false;
    Vector3 targetPos;

    public Animator npcAnimator;

    public Dialogue dialogue;

    public HealthBarControll HealthBar;

    private void Start()
    {
        GameObject.FindObjectOfType<LanguageManager>().getCorrectName("BfCabCrew", null,null, dialogue);
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasToRun)
        {
            if (stamina > 5 && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                npcAnimator.SetBool("hasToThrow", true);
            }
            else
            {
                if (Time.time > coolDownRate)
                {
                    stamina = 30;
                }
            }
        }
        else
        {
            if (!isDestinationSet)
            {
                targetPos = GameObject.Find("cabCrewDestination").gameObject.transform.position;
                npcAnimator.SetBool("hasToRun", true);
                isDestinationSet = true;
            }
            else
            {
                if(targetPos.x == gameObject.transform.position.x && targetPos.y == gameObject.transform.position.y)
                {
                    gameObject.SetActive(false);
                    GameObject.FindObjectOfType<PlayerController>().isCabCrewDead = true;
                }
            }

        }        

    }

    private void FixedUpdate()
    {
        if (!hasToRun)
        {
            Vector2 semenPosition = GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
            Vector2 aimDirection = semenPosition - rb.position;
            float aimAngel = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngel;
        }
        else
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, 0.2f);
            rb.rotation = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg - 90f;
        }

    }

    void fireNextBottle()
    {

        stamina = stamina - 5;
        weapon.Fire(stamina);
        npcAnimator.SetBool("hasToThrow", false);

    }

}
