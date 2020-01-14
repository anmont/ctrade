using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class uiCityScript6 : MonoBehaviour
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
            if (globals.selectedVessel == null)
            {
                aAA.text = globals.cityList[6].name;
            }
            else
            {
                aAA.text = globals.cityList[6].name + " (" + Mathf.RoundToInt(Vector3.Distance(globals.cityList[6].transform.position,globals.selectedVessel.transform.position)) + "km)";
            }
        }

    }

    public void navigateTo()
    {
        if (globals.selectedVessel != null)
        {
            NavMeshAgent agent = globals.selectedVessel.GetComponent<NavMeshAgent>();
            agent.SetDestination(globals.cityList[6].transform.position);
        }
    }
}
