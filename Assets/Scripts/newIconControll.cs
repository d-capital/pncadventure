using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newIconControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void indicateThereAreUnredTasks()
    {
        gameObject.SetActive(true);
        //TODO: play sound
    }
}
