using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shipguiLabelFacing : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCamera;
    public Transform thisUI;
    public GameObject ship;
    void Start()
    {
        mainCamera = Camera.main;
        this.transform.parent.GetComponent<Canvas>().worldCamera = mainCamera;
        ship = this.transform.parent.transform.parent.gameObject;
        thisUI = this.transform;
        thisUI.Find("Text").GetComponent<Text>().text = ship.name;
    }

    // Update is called once per frame
    void LateUpdate()
    {
            //Quaternion v = mainCamera.transform.rotation - transform.rotation;
            //v.x = v.z = 0.0f;
            transform.LookAt( -mainCamera.transform.position ); 
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            transform.rotation = rotation;
            
        
    }
}
