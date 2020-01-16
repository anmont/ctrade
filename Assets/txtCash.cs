using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class txtCash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string stringA = globals.playerCash.ToString("C0");
        //String.Format("{0:C0}", Convert.ToInt32(stringA));
        //stringA = "¢" + stringA;
        this.GetComponent<Text>().text = stringA;
    }
}
