using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class uiCityScript5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (globals.cityList.Count > 9)
        {
            Text aAA = this.gameObject.GetComponentInChildren<Text>();
            GameObject myShip = GameObject.FindGameObjectWithTag("Player");
            aAA.text = globals.cityList[5].name + " (" + Mathf.RoundToInt(Vector3.Distance(globals.cityList[5].transform.position,myShip.transform.position)) + "km)";
        }

    }

    public void navigateTo()
    {
        NavMeshAgent agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
        agent.SetDestination(globals.cityList[5].transform.position);
    }
}
