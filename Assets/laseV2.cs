using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class laseV2 : MonoBehaviour
{
    public LineRenderer laser;
    private RaycastHit hit;
    //public LineRenderer paint;
    public GameObject player;
    private GameObject target;
    private GameObject cam;
    public GameObject[] paints;

    private bool scaling = false;
    public bool pixelated = false;
    public bool darkTriggered = false;
    private bool movingInX = false;
    private bool movingInY = false;
    private bool movingInZ = false;
    private bool rotateInX = false;
    private bool rotateInY = false;
    private bool rotateInZ = false;
    public bool useApple = false;
    public bool usePear = false;
    public bool useKiwi = false;

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
        laser.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    // Choose skybox
                    if (hit.collider.tag == "skybox")
                    {
                        Material skybox = Resources.Load("skyboxs/" + hit.collider.name, typeof(Material)) as Material;
                        RenderSettings.skybox = skybox;
                    }

                    // Use apple
                    if (hit.collider.tag == "apple")
                    {
                        if (useApple)
                        {
                            GameObject.Find("DrawLineManager").GetComponent<PaintManager>().useApple = false;
                            useApple = false;
                        }
                        else
                        {
                            GameObject.Find("DrawLineManager").GetComponent<PaintManager>().useApple = true;
                            useApple = true;
                        }
                    }

                    // Use pear
                    if (hit.collider.tag == "pear")
                    {
                        if (useApple)
                        {
                            GameObject.Find("DrawLineManager").GetComponent<PaintManager>().usePear = false;
                            usePear = false;
                        }
                        else
                        {
                            GameObject.Find("DrawLineManager").GetComponent<PaintManager>().usePear = true;
                            usePear = true;
                        }
                    }

                    // Use apple
                    if (hit.collider.tag == "kiwi")
                    {
                        if (useKiwi)
                        {
                            GameObject.Find("DrawLineManager").GetComponent<PaintManager>().useKiwi = false;
                            useKiwi = false;
                        }
                        else
                        {
                            GameObject.Find("DrawLineManager").GetComponent<PaintManager>().useKiwi = true;
                            useKiwi = true;
                        }
                    }

                    // Enable/disable glow in dark view
                    if (hit.collider.tag == "glow")
                    {
                        if (hit.collider.gameObject.GetComponent<glowInDarkManager>().darkTriggered)
                        {
                            hit.collider.gameObject.GetComponent<glowInDarkManager>().darkTriggered = false;
                            darkTriggered = false;
                            Material skybox = Resources.Load("skyboxs/skybox1", typeof(Material)) as Material;
                            RenderSettings.skybox = skybox;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<glowInDarkManager>().darkTriggered = true;
                            darkTriggered = true;
                        }
                    }

                    // Enable/disable pixelated in dark view
                    if (hit.collider.tag == "pixelated")
                    {
                        if (hit.collider.gameObject.GetComponent<pixelatedManager>().pixelatedTriggered)
                        {
                            hit.collider.gameObject.GetComponent<pixelatedManager>().pixelatedTriggered = false;
                            pixelated = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<pixelatedManager>().pixelatedTriggered = true;
                            pixelated = true;
                        }
                    }


                    // Teleport
                    if (hit.collider.tag == "ground")
                    {
                        player.transform.position += hit.collider.transform.position;
                    }
                    // Erase all
                    if (hit.collider.tag == "eraser")
                    {
                        DestroyAll("paint");
                    }
                    // Erase one
                    if (hit.collider.tag == "paint")
                    {
                        GetAllGameObjectsWithSameTag(hit.collider.tag);
                        string masterName = hit.collider.gameObject.name;
                        for (var i = 0; i < paints.Length; i++)
                        {
                            if (paints[i].name == masterName)
                            {
                                Destroy(paints[i]);
                            }
                        }
                    }
                    // Enable scaling
                    if (hit.collider.tag == "scale")
                    {
                        if(hit.collider.gameObject.GetComponent<scaleManager>().scaleTriggered)
                        {
                            hit.collider.gameObject.GetComponent<scaleManager>().scaleTriggered = false;
                            scaling = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<scaleManager>().scaleTriggered = true;
                            scaling = true;
                        }
                    }
                    // Enable moving in X
                    if (hit.collider.tag == "moveX")
                    {
                        if (hit.collider.gameObject.GetComponent<moveXManager>().moveXTriggered)
                        {
                            hit.collider.gameObject.GetComponent<moveXManager>().moveXTriggered = false;
                            movingInX = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<moveXManager>().moveXTriggered = true;
                            movingInX = true;
                        }
                    }
                    // Enable moving in Y
                    if (hit.collider.tag == "moveY")
                    {
                        if (hit.collider.gameObject.GetComponent<moveYManager>().moveYTriggered)
                        {
                            hit.collider.gameObject.GetComponent<moveYManager>().moveYTriggered = false;
                            movingInY = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<moveYManager>().moveYTriggered = true;
                            movingInY = true;
                        }
                    }
                    // Enable moving in Z
                    if (hit.collider.tag == "moveZ")
                    {
                        if (hit.collider.gameObject.GetComponent<moveZManager>().moveZTriggered)
                        {
                            hit.collider.gameObject.GetComponent<moveZManager>().moveZTriggered = false;
                            movingInZ = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<moveZManager>().moveZTriggered = true;
                            movingInZ = true;
                        }
                    }
                    // Enable rotation in X
                    if (hit.collider.tag == "rotateX")
                    {
                        if (hit.collider.gameObject.GetComponent<rotateXManager>().rotateXTriggered)
                        {
                            hit.collider.gameObject.GetComponent<rotateXManager>().rotateXTriggered = false;
                            rotateInX = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<rotateXManager>().rotateXTriggered = true;
                            rotateInX = true;
                        }
                    }
                    // Enable rotation in Y
                    if (hit.collider.tag == "rotateY")
                    {
                        if (hit.collider.gameObject.GetComponent<rotateYManager>().rotateYTriggered)
                        {
                            hit.collider.gameObject.GetComponent<rotateYManager>().rotateYTriggered = false;
                            rotateInY = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<rotateYManager>().rotateYTriggered = true;
                            rotateInY = true;
                        }
                    }
                    // Enable rotation in Z
                    if (hit.collider.tag == "rotateZ")
                    {
                        if (hit.collider.gameObject.GetComponent<rotateZManager>().rotateZTriggered)
                        {
                            hit.collider.gameObject.GetComponent<rotateZManager>().rotateZTriggered = false;
                            rotateInZ = false;
                        }
                        else
                        {
                            hit.collider.gameObject.GetComponent<rotateZManager>().rotateZTriggered = true;
                            rotateInZ = true;
                        }
                    }

                }

                if (OVRInput.GetDown(OVRInput.Button.Three))
                {
                    if (scaling)
                    {
                        ScaleAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, 5f);
                    }
                    if (rotateInX)
                    {
                        RotateXAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, Vector3.right);
                    }
                    if (rotateInY)
                    {
                        RotateYAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, Vector3.up);
                    }
                    if (rotateInZ)
                    {
                        RotateZAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, Vector3.forward);
                    }
                    if (movingInY)
                    {
                        MoveYAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, -0.5f);
                    }
                    if (movingInX)
                    {
                        MoveXAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, -0.5f);
                    }
                    if (movingInZ)
                    {
                        MoveZAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, -0.5f);
                    }
                }
                if (OVRInput.GetDown(OVRInput.Button.Four))
                {
                    if (scaling)
                    {
                        ScaleAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, -5f);
                    }
                    if (rotateInX)
                    {
                        RotateXAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, Vector3.left);
                    }
                    if (rotateInY)
                    {
                        RotateYAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, Vector3.down);
                    }
                    if (rotateInZ)
                    {
                        RotateZAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, Vector3.back);
                    }
                    if (movingInY)
                    {
                        MoveYAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, 0.5f);
                    }
                    if (movingInX)
                    {
                        MoveXAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, 0.5f);
                    }
                    if (movingInZ)
                    {
                        MoveZAll(hit.collider.gameObject.tag, hit.collider.gameObject.name, 0.5f);
                    }
                }
                laser.SetPosition(1, hit.point);

            }
        }
        else laser.SetPosition(1, transform.forward * 5000);
    }

    void DestroyAll(string tag)
    {
        paints = GameObject.FindGameObjectsWithTag(tag);
        for (var i = 0; i < paints.Length; i++)
        {
            Destroy(paints[i]);
        }
    }

    void GetAllGameObjectsWithSameTag(string tag)
    {
        paints = GameObject.FindGameObjectsWithTag(tag);
    }

    void ScaleAll(string tag, string name, float scale)
    {
        GetAllGameObjectsWithSameTag(tag);
        string masterName = name;
        Debug.Log(masterName);
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                Debug.Log(paints[i].transform.localScale.x > 0.001f);
                if (paints[i].transform.localScale.x > 0.001f)
                {
                    paints[i].transform.localScale += new Vector3(scale, scale, scale);

                }
            }
            
        }
    }

    void MoveXAll(string tag, string name, float moveX)
    {
        GetAllGameObjectsWithSameTag(tag);
        string masterName = name;
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                paints[i].transform.Translate(new Vector3(moveX, 0, 0) * Time.smoothDeltaTime);
            }
        }
    }

    void MoveYAll(string tag, string name, float moveY)
    {
        GetAllGameObjectsWithSameTag(tag);
        string masterName = name;
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                paints[i].transform.Translate(new Vector3(0, moveY, 0) * Time.smoothDeltaTime);
            }
        }
    }

    void MoveZAll(string tag, string name, float moveZ)
    {
        GetAllGameObjectsWithSameTag(tag);
        string masterName = name;
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                paints[i].transform.Translate(new Vector3(0, 0, moveZ) * Time.smoothDeltaTime);
            }
        }
    }

    void RotateXAll(string tag, string name, Vector3 rot)
    {
        GetAllGameObjectsWithSameTag(tag);
        Transform parentTransform = GameObject.Find("parent" + name).transform;
        string masterName = name;
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                paints[i].transform.RotateAround(parentTransform.position, rot, 50 * Time.deltaTime);
            }
        }
    }

    void RotateYAll(string tag, string name, Vector3 rot)
    {
        GetAllGameObjectsWithSameTag(tag);
        Transform parentTransform = GameObject.Find("parent" + name).transform;
        string masterName = name;
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                paints[i].transform.RotateAround(parentTransform.position, rot, 50 * Time.deltaTime);
            }
        }
    }

    void RotateZAll(string tag, string name, Vector3 rot)
    {
        GetAllGameObjectsWithSameTag(tag);
        Transform parentTransform = GameObject.Find("parent" + name).transform;
        string masterName = name;
        for (var i = 0; i < paints.Length; i++)
        {
            if (paints[i].name == masterName)
            {
                paints[i].transform.RotateAround(parentTransform.position, rot, 50 * Time.deltaTime);
            }
        }
    }



}
