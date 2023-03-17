using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newIconControll : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void indicateThereAreUnredTasks()
    {
        gameObject.SetActive(true);
        audioSource.Play();
    }
}
