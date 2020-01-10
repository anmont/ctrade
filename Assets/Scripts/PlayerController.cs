using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //public Camera mycam;
    private Transform myCamera = null; 
    private Vector3 dest;
    public static float speed = 8f;
    public Camera mainCamera;
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        mainCamera = myCamera.GetComponent<Camera>();
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal") )
        {
            dest = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            myCamera.position = myCamera.position += (dest * speed);
            
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray castme = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(castme, out hit))
            {
                    agent.SetDestination(hit.point);
            }
        }
    }
}
