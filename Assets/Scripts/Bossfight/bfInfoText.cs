using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bfInfoText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(hideInfoTextWithDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator hideInfoTextWithDelay()
    {
        yield return new WaitForSeconds(5.0f);
        gameObject.SetActive(false);
    }
}
