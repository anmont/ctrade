using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiShipScript : MonoBehaviour
{
    public GameObject thisVessel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void selectThisVessel()
    {
        globals.selectedVessel = thisVessel;
        Camera.main.gameObject.transform.position = new Vector3(thisVessel.transform.position.x, Camera.main.gameObject.transform.position.y, thisVessel.transform.position.z) ;

    }
}
