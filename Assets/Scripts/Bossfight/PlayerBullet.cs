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
            if (target.GetComponent<CabCrewController>().health > 0)
            {
                target.GetComponent<CabCrewController>().health -= 10;
            }
            else
            {
                Destroy(target.GetComponent<CabCrewController>().gameObject);
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
            if (target.GetComponent<ChairControll>().health > 0)
            {
                target.GetComponent<ChairControll>().health -= 10;
            }
            else
            {
                Destroy(target.GetComponent<ChairControll>().gameObject);
            }
        }
    }
}
