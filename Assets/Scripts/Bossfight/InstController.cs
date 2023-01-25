using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CloseInst());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CloseInst()
    {
        yield return new WaitForSeconds(10);
        {
            gameObject.SetActive(false);
        }
    }
}
