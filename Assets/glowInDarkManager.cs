using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowInDarkManager : MonoBehaviour
{
    public bool darkTriggered = false;
    public GameObject scaleGO;
    private GameObject[] paints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(darkTriggered)
        {
            scaleGO.GetComponent<Renderer>().material.color = Color.yellow;
            scaleGO.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");

            Material skybox = Resources.Load("SS_2048", typeof(Material)) as Material;
            RenderSettings.skybox = skybox;

            paints = GameObject.FindGameObjectsWithTag("paint");
            for (var i = 0; i < paints.Length; i++)
            {
                paints[i].GetComponent<Material>().EnableKeyword("_EMISSION");
            }
        } else
        {
            scaleGO.GetComponent<Renderer>().material.color = Color.white;

            

            paints = GameObject.FindGameObjectsWithTag("paint");
            for (var i = 0; i < paints.Length; i++)
            {
                paints[i].GetComponent<Material>().DisableKeyword("_EMISSION");
            }
        }
    }
}
