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
    public NavMeshAgent agent;
    public bool pointerOnUi = false;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        mainCamera = myCamera.GetComponent<Camera>();
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }
public void onPointerExit()
{
    pointerOnUi = false;
}
public void onPointerOver()
{
    pointerOnUi = true;
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
            
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                Vector3 temp = new Vector3(myCamera.position.x,myCamera.position.y,myCamera.position.z);
                Vector3 desiredHeight = temp += (new Vector3(0f, -Input.mouseScrollDelta.y, 0f) * (speed / 2));

                if (desiredHeight.y > cameraFloor && desiredHeight.y < cameraCeiling)
                {
                    myCamera.position = desiredHeight;
                }
            }



            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Over Game Object : " + EventSystem.current.IsPointerOverGameObject());
                Ray castme = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                
                if (Physics.Raycast(castme, out hit))
                {
                    if (!pointerOnUi)
                    {
                        agent.SetDestination(hit.point);
                    }
                }
                
            }
        }
    }
}
