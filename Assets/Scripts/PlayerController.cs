using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    //public Camera mycam;
    private Transform myCamera = null; 
    private Vector3 dest;
    public static float speed = 8f;
    public float cameraCeiling = 250f;
    public float cameraFloor = 10f;
    public Camera mainCamera;
    public GameObject tradeRoutePanelObh;
    //public NavMeshAgent agent;
    public bool pointerOnUi = false;
    public bool edgeScrolling = true;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        mainCamera = myCamera.GetComponent<Camera>();
        //agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }
public void onPointerExit()
{
    pointerOnUi = false;
    //Debug.Log("pointer on ui");
}

//public void meshCellDensity(float newHeight)
//{

//    Renderer myWater = GameObject.Find("visibleWaterPlane").GetComponent<MeshRenderer>();
//    float setheight = (newHeight * 1.542f);
    //myWater.GetComponent<Shader>().FindPassTagValue;
//    myWater.material.SetFloat("cellDensity", setheight);


//}
public void onPointerOver()
{
    pointerOnUi = true;
    //Debug.Log("pointer left ui");
}
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.mousePosition.x.ToString() + "," + Input.mousePosition.y.ToString() + "," + Input.mousePosition.z.ToString());
        //Screen.height
        //Screen.width
        if (edgeScrolling)
        {
            if(Input.mousePosition.x < 3)
            {
                if (myCamera.position.x > -490)
                {
                    //pan camera left while camera is not < 0
                    dest = new Vector3(-.1f, 0.0f, 0f);
                    float timeSpeed = timeBehavior.currentTimeScale;
                    if (timeSpeed < 1) { timeSpeed = 1; }
                    myCamera.position = myCamera.position += (dest * speed)/timeSpeed;
                }
            }
            
            if(Input.mousePosition.x > (Screen.width - 3))
            {
                if (myCamera.position.x < 490)
                {
                    //pan camera right while camera is not < 0
                    dest = new Vector3(0.1f, 0.0f, 0f);
                    float timeSpeed = timeBehavior.currentTimeScale;
                    if (timeSpeed < 1) { timeSpeed = 1; }
                    myCamera.position = myCamera.position += (dest * speed)/timeSpeed;
                }
            }

            if(Input.mousePosition.y < 3)
            {
                if (myCamera.position.z > -490)
                {
                    //pan camera left while camera is not < 0
                    dest = new Vector3(0f, 0.0f, -0.1f);
                    float timeSpeed = timeBehavior.currentTimeScale;
                    if (timeSpeed < 1) { timeSpeed = 1; }
                    myCamera.position = myCamera.position += (dest * speed)/timeSpeed;
                }
            }
            
            if(Input.mousePosition.y > (Screen.height - 3))
            {
                //pan camera right while camera is not < 0
                if (myCamera.position.z < 490 )
                {
                    dest = new Vector3(0f, 0.0f, 0.1f);
                    float timeSpeed = timeBehavior.currentTimeScale;
                    if (timeSpeed < 1) { timeSpeed = 1; }
                    myCamera.position = myCamera.position += (dest * speed)/timeSpeed;
                }
            }
        }


        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal") )
        {
            dest = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            float timeSpeed = timeBehavior.currentTimeScale;
            if (timeSpeed < 1) { timeSpeed = 1; }
            myCamera.position = myCamera.position += (dest * speed)/timeSpeed;
            
        }
        if (Input.GetKeyUp(KeyCode.F5))
        {
            string datestring = timeBehavior.gameDate.Year + "_" + timeBehavior.gameDate.Month + "_" + timeBehavior.gameDate.Day;
            StaticClass.saveFileName = "quicksave" + datestring + ".trdr";
            if (StaticClass.savePath == null)
            {
                StaticClass.savePath = Application.persistentDataPath.ToString() + "/saves/";
            }
            globals.saveInstance.save();
        }
        if (!escMenuHandler.escapeMenuVisible)
        {

            
            if (!pointerOnUi)
            {
                if (Input.mouseScrollDelta != Vector2.zero)
                {
                    Vector3 temp = new Vector3(myCamera.position.x,myCamera.position.y,myCamera.position.z);
                    Vector3 desiredHeight = temp += (new Vector3(0f, -Input.mouseScrollDelta.y, 0f) * (speed / 2));

                    if (desiredHeight.y > cameraFloor && desiredHeight.y < cameraCeiling)
                    {
                        myCamera.position = desiredHeight;
                    }
                    //meshCellDensity(desiredHeight.y);
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //GameObject panel = GameObject.Find("tradeRoutePanel");
                //GameObject panel = transform.Find("tradeRoutePanel").parent.gameObject;
                //transform.Find()
                
                if (tradeRoutePanelObh.activeInHierarchy)
                {
                    tradeRoutePanelObh.SetActive(false);
                }
                else
                {
                    tradeRoutePanelObh.SetActive(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (globals.selectedVessel == null)
                {
                    if (globals.shipList.Count > 0)
                    {
                        globals.selectedVessel = globals.shipList[0];
                        Camera.main.gameObject.transform.position = new Vector3(globals.selectedVessel.transform.position.x, Camera.main.gameObject.transform.position.y, globals.selectedVessel.transform.position.z) ;
                    }
                }
                else if (globals.shipList.Count > 1)
                {
                    int vesselM = globals.shipList.IndexOf(globals.selectedVessel);
                    if (globals.shipList.Count == vesselM+1)
                    {
                        globals.selectedVessel = globals.shipList[0];
                        Camera.main.gameObject.transform.position = new Vector3(globals.selectedVessel.transform.position.x, Camera.main.gameObject.transform.position.y, globals.selectedVessel.transform.position.z) ;
                    }
                    else
                    {
                        globals.selectedVessel = globals.shipList[vesselM+1];
                        Camera.main.gameObject.transform.position = new Vector3(globals.selectedVessel.transform.position.x, Camera.main.gameObject.transform.position.y, globals.selectedVessel.transform.position.z) ;
                    }

                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                //Debug.Log("Over Game Object : " + EventSystem.current.IsPointerOverGameObject());
                Ray castme = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(castme, out hit))
                {
                    if (!pointerOnUi)
                    {
                        if (hit.transform.gameObject.tag == "City")
                        {
                            // if right click on city open city properties
                            globals.openCityProperties(hit.transform.gameObject);
                        }
                        else if (hit.transform.gameObject.tag == "Player")
                        {
                            //if right click on ship open ship properties
                            globals.openShipDetails(hit.transform.gameObject);
                        }
                    }

                }

            }


            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Over Game Object : " + EventSystem.current.IsPointerOverGameObject());
                Ray castme = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                
                if (Physics.Raycast(castme, out hit))
                {
                    if (!pointerOnUi)
                    {
                        //click is not on UI.... lets decide what to do
                        //Tag is Player or tag is City
                        //Debug.Log(hit.transform.gameObject.name.ToString());
                        if (hit.transform.gameObject.tag == "City")
                        {
                            if (globals.selectedVessel != null && globals.selectedVessel.gameObject.transform.parent.name == "shipGroup")
                            {
                                globals.selectedVessel.gameObject.GetComponent<NavMeshAgent>().SetDestination(hit.transform.position);
                                //globals.selectedVessel.gameObject.GetComponent<NavMeshAgent>()..SetDestination(hit.point);
                            }
                        }
                        else if (hit.transform.gameObject.tag == "Player")
                        {
                            //if same vessel... do nothing for now but open properties
                            if (hit.transform.gameObject.name == globals.selectedVessel.gameObject.name)
                            {
                                //This probably wont do anything... left clicking is for actions
                                //right clicking is for props >> so refer to the on buttondown 1 for the properties
                                //globals.openShipDetails(hit.transform.gameObject);
                            }
                            else
                            {
                                hit.transform.gameObject.GetComponent<uiShipScript>().selectThisVessel();
                                //globals.selectedVessel = hit.transform.gameObject;
                                //Camera.main.gameObject.transform.position = new Vector3(globals.selectedVessel.transform.position.x, Camera.main.gameObject.transform.position.y, globals.selectedVessel.transform.position.z) ;
                            }
                            //If another vessel ... select that vessel as active
                        }
                        else if (hit.transform.gameObject.tag == "ai")
                        {
                            //if same vessel... do nothing for now but open properties
                            if (hit.transform.gameObject.name == globals.selectedVessel.gameObject.name)
                            {
                                globals.openShipDetails(hit.transform.gameObject);
                                //Debug.Log("I shoul have opened the ship panel");
                            }
                            else
                            {
                                hit.transform.gameObject.GetComponent<aiShipScript>().selectThisVessel();
                                //globals.selectedVessel = hit.transform.gameObject;
                                //Camera.main.gameObject.transform.position = new Vector3(globals.selectedVessel.transform.position.x, Camera.main.gameObject.transform.position.y, globals.selectedVessel.transform.position.z) ;
                            }
                            //If another vessel ... select that vessel as active
                        }
                        else if (globals.selectedVessel != null && globals.selectedVessel.gameObject.transform.parent.name == "shipGroup")
                        {
                            globals.selectedVessel.gameObject.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                            //globals.selectedVessel.gameObject.GetComponent<NavMeshAgent>()..SetDestination(hit.point);
                        } 
                        //click on ship that is selected vessel (vessel properties)

                        //click on ship that is not selected vessel (select that vessel)
                        //Click on a city (move vessel to the city)

                        
                        //agent.SetDestination(hit.point);
                    }
                }
                
            }
        }
    }
}
