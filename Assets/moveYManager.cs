using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveYManager : MonoBehaviour
{
    public bool moveYTriggered = false;
    public GameObject scaleGO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moveYTriggered)
        {
            scaleGO.GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            scaleGO.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
