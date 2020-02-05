using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnShip : MonoBehaviour
{
    public GameObject thisVessel;
    public Button childButton;

    public void Update()
    {
        if (thisVessel.GetComponent<uiShipScript>().cityAtAnchor == null)
        {
            childButton.gameObject.SetActive(false);
        }
        else
        {
            childButton.gameObject.SetActive(true);
        }
    }

    
}
