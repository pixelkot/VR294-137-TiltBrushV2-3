using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour
{
    public LineRenderer currLine;
    private GameObject leftController;
    private GameObject rightController;
    private int numClicks = 0;
    private GameObject colorIndicator;
    public Transform rightGripTransform;
    public Material paintMat;

    float width = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        leftController = GameObject.Find("CustomHandLeft");
        rightController = GameObject.Find("CustomHandRight");
        colorIndicator = GameObject.Find("ColorIndicator");

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.AddComponent<Rigidbody>();
        cube.transform.position = rightGripTransform.position;
        cube.transform.parent = rightGripTransform;
        cube.transform.localScale = new Vector3(0.05f, 0.05f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            width += 0.05f;
            Debug.Log(width);
        }
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            if (width > 0.05f)
            {
             width -= 0.05f;
             Debug.Log(width);
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            //Debug.Log("test pulled trigger");
            GameObject go = new GameObject();   
            currLine = go.AddComponent<LineRenderer>();
            currLine.SetWidth(width, width);

            go.tag = "paint";
            
            BoxCollider box = go.AddComponent<BoxCollider>();
            //box.size = new Vector3(width, width, 0.01f);
            box.size = new Vector3(1f, 1f, 1f);
            box.transform.position = go.transform.position;
            //box.tag = "paint";
            box.isTrigger = true;

            //Debug.Log(colorIndicator.GetComponent<ColorIndicator>().color.ToColor());
            currLine.material.color = colorIndicator.GetComponent<ColorIndicator>().color.ToColor();
            numClicks = 0;
        } else if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            currLine.SetVertexCount(numClicks + 1);
            currLine.SetPosition(numClicks, leftController.transform.position);
            numClicks++;
        }

     
    }
}
