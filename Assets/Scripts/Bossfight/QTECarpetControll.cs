using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTECarpetControll : MonoBehaviour
{

    private void Update()
    {
        Vector3 playerPos = GameObject.FindObjectOfType<PlayerController>().transform.position;
        if (playerPos.y >= transform.position.y)
        {
            ActivateQte();
        } 
    }

    private void ActivateQte()
    {

        if (!GameObject.FindObjectOfType<GameOver>().isGameOverScreenShown)
        {
            QTEController[] qteControllers = Resources.FindObjectsOfTypeAll<QTEController>();
            foreach (QTEController i in qteControllers)
            {
                i.gameObject.SetActive(true);
            }
        }  

    }
}
