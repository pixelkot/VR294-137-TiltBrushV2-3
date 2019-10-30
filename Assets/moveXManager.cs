using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveXManager : MonoBehaviour
{
    public bool moveXTriggered = false;
    public GameObject scaleGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveXTriggered)
        {
            scaleGO.GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            scaleGO.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
