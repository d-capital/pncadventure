using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
        //check if enemy was hit and register damage
        GameObject target = collision.collider.gameObject;
        if (target.name == "CabCrew")
        {
            ShowEnemyHealthBar();
            target.GetComponent<CabCrewController>().HealthBar.ResetNameAndHealth(
                target.GetComponent<CabCrewController>().health, 
                target.GetComponent<CabCrewController>().Name);
            if (target.GetComponent<CabCrewController>().health > 0)
            {
                target.GetComponent<CabCrewController>().health -= 10;
                target.GetComponent<CabCrewController>().HealthBar.SetHealth(target.GetComponent<CabCrewController>().health);
            }
            else
            {
                Destroy(target.GetComponent<CabCrewController>().gameObject);
                HideEnemyHealthBar();
            }
        }
        //if (target.name == "Major")
        //{
          //  if (target.GetComponent<MajorController>().health > 0)
            //{
              //  target.GetComponent<MajorController>().health -= 10;
            //}
            //else
            //{
              //  Destroy(target.GetComponent<MajorController>().gameObject);
            //}
        //}
        if (target.name == "chair")
        {
            ShowEnemyHealthBar();
            target.GetComponent<ChairControll>().HealthBar.ResetNameAndHealth(
                target.GetComponent<ChairControll>().health,
                target.GetComponent<ChairControll>().Name);
            if (target.GetComponent<ChairControll>().health > 0)
            {
                target.GetComponent<ChairControll>().health -= 10;
                target.GetComponent<ChairControll>().HealthBar.SetHealth(target.GetComponent<ChairControll>().health);
            }
            else
            {
                Destroy(target.GetComponent<ChairControll>().gameObject);
                HideEnemyHealthBar();
            }
        }
    }

    private void ShowEnemyHealthBar()
    {
        HealthBarControll[] healthBars = Resources.FindObjectsOfTypeAll<HealthBarControll>();
        foreach (HealthBarControll i in healthBars)
        {
            if (i.gameObject.name == "EnemyHealthBar")
            {
                i.gameObject.SetActive(true);
            }
        }
    }

    private void HideEnemyHealthBar()
    {
        HealthBarControll[] healthBarControlls = GameObject.FindObjectsOfType<HealthBarControll>();
        foreach (HealthBarControll i in healthBarControlls)
        {
            if (i.gameObject.name == "EnemyHealthBar")
            {
                i.gameObject.SetActive(false);
            }
        }
    }
}
