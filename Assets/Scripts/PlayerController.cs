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

public void meshCellDensity(float newHeight)
{

    Renderer myWater = GameObject.Find("visibleWaterPlane").GetComponent<MeshRenderer>();
    float setheight = (newHeight * 1.542f);
    //myWater.GetComponent<Shader>().FindPassTagValue;
    myWater.material.SetFloat("cellDensity", setheight);


}
public void onPointerOver()
{
    pointerOnUi = true;
    //Debug.Log("pointer left ui");
}
    // Update is called once per frame
    void Update()
    {
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
                    meshCellDensity(desiredHeight.y);
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
                        if (globals.selectedVessel != null)
                        {
                            globals.selectedVessel.gameObject.GetComponent<NavMeshAgent>().SetDestination(hit.point);
                            //globals.selectedVessel.gameObject.GetComponent<NavMeshAgent>()..SetDestination(hit.point);
                        }
                        //agent.SetDestination(hit.point);
                    }
                }
                
            }
        }
    }
}
