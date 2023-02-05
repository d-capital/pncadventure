using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGridControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void removeAllTasks()
    {
        if (gameObject.transform.childCount > 0)
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

    }
}
