using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintManager : MonoBehaviour
{
    private GameObject leftController;
    private GameObject colorIndicator;
    private Color color;
    private int index = 0;
    public Material paintMat;
    private GameObject parent;
    public bool isPixelated = false;
    public bool useApple = false;
    public bool usePear = false;
    public bool useKiwi = false;

    float width = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        leftController = GameObject.Find("CustomHandLeft");
        colorIndicator = GameObject.Find("ColorIndicator");
    }

    // Update is called once per frame
    void Update()
    {
        isPixelated = GameObject.Find("laserp").GetComponent<laseV2>().pixelated;
        color = colorIndicator.GetComponent<ColorIndicator>().color.ToColor();

        // Stroke width up
        if (OVRInput.GetDown(OVRInput.Button.Four) && !GameObject.Find("scaleTrigger").GetComponent<scaleManager>().scaleTriggered
            && !GameObject.Find("moveX").GetComponent<moveXManager>().moveXTriggered
            && !GameObject.Find("moveY").GetComponent<moveYManager>().moveYTriggered
            && !GameObject.Find("moveZ").GetComponent<moveZManager>().moveZTriggered
            && !GameObject.Find("rotateX").GetComponent<rotateXManager>().rotateXTriggered
            && !GameObject.Find("rotateY").GetComponent<rotateYManager>().rotateYTriggered
            && !GameObject.Find("rotateZ").GetComponent<rotateZManager>().rotateZTriggered)
        {
            width += 0.01f;
        }

        // Stroke width down
        if (OVRInput.GetDown(OVRInput.Button.Three) && !GameObject.Find("scaleTrigger").GetComponent<scaleManager>().scaleTriggered
            && !GameObject.Find("moveX").GetComponent<moveXManager>().moveXTriggered
            && !GameObject.Find("moveY").GetComponent<moveYManager>().moveYTriggered
            && !GameObject.Find("moveZ").GetComponent<moveZManager>().moveZTriggered
            && !GameObject.Find("rotateX").GetComponent<rotateXManager>().rotateXTriggered
            && !GameObject.Find("rotateY").GetComponent<rotateYManager>().rotateYTriggered
            && !GameObject.Find("rotateZ").GetComponent<rotateZManager>().rotateZTriggered)
        {
            if (width > 0.01f)
            {
                width -= 0.01f;
            }
        }


        
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            parent = GameObject.CreatePrimitive(PrimitiveType.Cube);
            parent.transform.position = leftController.transform.position;
            parent.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            parent.tag = "paint";
            parent.name = "parentpaint" + " " + index.ToString();
        }

        // Draw 
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (useApple)
            {
                GameObject go = Instantiate(Resources.Load("Apple")) as GameObject;
                go.transform.position = leftController.transform.position;
                go.transform.localScale = new Vector3(width * 100, width * 100, width * 100);
                go.tag = "paint";
                go.name = "paint" + " " + index.ToString();

                go.transform.parent = parent.transform;
            }
            else if (usePear)
            {
                GameObject go = Instantiate(Resources.Load("Pear")) as GameObject;
                go.transform.position = leftController.transform.position;
                go.transform.localScale = new Vector3(width * 100, width * 100, width * 100);
                go.tag = "paint";
                go.name = "paint" + " " + index.ToString();

                go.transform.parent = parent.transform;
            }
            else if (useKiwi)
            {
                GameObject go = Instantiate(Resources.Load("KiwiHalf")) as GameObject;
                go.transform.position = leftController.transform.position;
                go.transform.localScale = new Vector3(width * 100, width * 100, width * 100);
                go.tag = "paint";
                go.name = "paint" + " " + index.ToString();

                go.transform.parent = parent.transform;
            }
            else if (isPixelated)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

                go.transform.position = leftController.transform.position;
                go.GetComponent<Renderer>().material = paintMat;
                go.GetComponent<Renderer>().material.color = color;
                go.transform.localScale = new Vector3(width, width, width);
                go.tag = "paint";
                go.name = "paint" + " " + index.ToString();

                go.transform.parent = parent.transform;
            } else
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                go.transform.position = leftController.transform.position;
                go.GetComponent<Renderer>().material = paintMat;
                go.GetComponent<Renderer>().material.color = color;
                go.transform.localScale = new Vector3(width, width, width);
                go.tag = "paint";
                go.name = "paint" + " " + index.ToString();

                go.transform.parent = parent.transform;
            }
            
        }

        // Separate drawings by names
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            index += 1;
        }


    }
}
