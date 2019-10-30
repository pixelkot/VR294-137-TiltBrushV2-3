using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class laserpointer : MonoBehaviour
{
    public LineRenderer laser;
    private RaycastHit hit;
    //public LineRenderer paint;
    public GameObject player;
    private GameObject target;
    private GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        //paint = GameObject.Find("DrawLineManager").GetComponent<DrawLineManager>().currLine;
        player = GameObject.Find("OVRPlayerController");
        cam = GameObject.Find("OVRCameraRig");
        //target = GameObject.Find("Target");
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            Debug.Log("pressing ------------------------------[");
            //Jump();
            //Debug.Log(GameObject.Find("New Game Object"));
            //GameObject obj = GameObject.Find("New Game Object");
            //obj.GetComponent<LineRenderer>().transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            //obj.GetComponent<LineRenderer>().SetPosition(1, Vector3.up);
        }

        laser.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                if (hit.collider.gameObject.tag == "paint")
                {
                    Debug.Log("collided with paint ----------------------");
                    if (OVRInput.GetDown(OVRInput.Button.Four))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    
                }
                if (hit.collider.gameObject.tag == "eraser")
                {
                    Debug.Log("collided with eraser ----------------------");
                    if (OVRInput.GetDown(OVRInput.Button.Four))
                    {
                        //Destroy("paint");
                    }

                }
                else
                {
                    laser.SetPosition(1, hit.point);
                }
                

            }
        }
        else laser.SetPosition(1, transform.forward * 5000);
    }

    //    private void OnTriggerEnter(Collider other)
    //    {
    //        if (other.tag == "paint")
    //        {
    //            Debug.Log("collided with paint ----------------------");
    //            Destroy(other.gameObject);
    //        }
    //    }

  

}
