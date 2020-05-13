﻿using System.Collections;
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
    //public NavMeshAgent agent;
    public bool pointerOnUi = false;

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
            if (Input.GetButton("Vertical") || Input.GetButton("Horizontal") )
            {
                dest = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                myCamera.position = myCamera.position += (dest * speed);
                
            }
            
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
