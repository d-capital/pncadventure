using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Weapon[] weapons = Resources.FindObjectsOfTypeAll<Weapon>();
        foreach (Weapon i in weapons)
        {
            if(i.gameObject.name == "semensWeapon")
            {
                i.gameObject.SetActive(true);
            }
        }
    }
}
