using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECarpetControll : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.name == "Player")
        {
            QTEController[] qteControllers = Resources.FindObjectsOfTypeAll<QTEController>();
            foreach (QTEController i in qteControllers)
            {
                i.gameObject.SetActive(true);
            }
        }

    }
}
