using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiCityNameLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = this.transform.parent.transform.parent.transform.parent.name.ToString();
        //Text string = this.GetComponent<Text>().text
        //Debug.Log("debug city: " + this.transform.parent.transform.parent.transform.parent.name.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
