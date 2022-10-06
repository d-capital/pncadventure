using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerGridControl : MonoBehaviour
{
    public GameObject gridGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void removeAllAnswers()
    {
        if (gridGameObject.transform.childCount > 0)
        {
            foreach (Transform child in gridGameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
