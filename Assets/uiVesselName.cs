﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiVesselName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (globals.selectedVessel != null)
        {
            this.gameObject.GetComponent<Text>().text = globals.selectedVessel.name;
        }
    }
}