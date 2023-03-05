using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>().animator.SetBool("hasWeapon", true);
        Weapon[] weapons = Resources.FindObjectsOfTypeAll<Weapon>();
        foreach (Weapon i in weapons)
        {
            if(i.gameObject.name == "semensWeapon")
            {
                i.gameObject.SetActive(true);
            }
        }
        weaponInfo[] weaponInfos = Resources.FindObjectsOfTypeAll<weaponInfo>();
        foreach (weaponInfo i in weaponInfos)
        {
            if (i.gameObject.name == "slyrf")
            {
                i.gameObject.SetActive(true);
            }
        }
        bfInfoText[] bfInfoTexts = Resources.FindObjectsOfTypeAll<bfInfoText>();
        foreach (bfInfoText i in bfInfoTexts)
        {
            if (i.gameObject.name == "bfInfoText")
            {
                i.gameObject.SetActive(true);
            }
        }
    }
}
