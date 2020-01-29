using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globals : MonoBehaviour
{

    public GameObject shipParentPanel;
    public static double playerCash = 100000;
    public static GameObject escapeMenu;
    public static GameObject selectedVessel;
    public static GameObject time;
    public static GameObject navMesh;
    public static List<GameObject> cityList = new List<GameObject>();
    public static List<GameObject> shipList = new List<GameObject>();
    public static List<string> vesselList = new List<string>();
    public static int vesselUpkeepMultiplyer = 75;
    public static double vesselShipSavings = .9;

    public static void calculateShipUpkeep()
    {
        foreach (GameObject vessel in shipList)
        {
            playerCash = playerCash - ((double)(vessel.GetComponent<uiShipScript>().vesselClassification + 1) * (double)vesselShipSavings * (double)vesselUpkeepMultiplyer);
            
        }
    }
    public static void createVessel(Vector3 location, int size)
    {
        string prefabName = "";
        if (size == 0)
        {
            prefabName = "smallShip";
        }
        else if (size == 1)
        {
            prefabName = "mediumShip";
        }
        else if (size == 2)
        {
            prefabName = "largeShip";
        }

        //innstantiate
        GameObject newVessel = (GameObject)Instantiate(Resources.Load(prefabName), location, Quaternion.identity);

        //Name the vessel
        int randMe = Random.Range(0,vesselList.Count);
        newVessel.name = vesselList[randMe];
        vesselList.RemoveAt(randMe);

        //update vessel list
        shipList.Add(newVessel);
        //GameObject buttonShip = null;

        //update GUI button for the vessel
        GameObject buttonShip = (GameObject)Instantiate(Resources.Load("shipButton"));
        //Button testB = Instantiate(Resources.Load("shipButton"));
        buttonShip.gameObject.transform.SetParent(GameObject.Find("guiVessels").transform);
        buttonShip.gameObject.transform.localScale = new Vector3(1,1,1);
        //buttonShip.gameObject.transform.parent = ; 
        newVessel.GetComponent<uiShipScript>().thisVessel = newVessel.gameObject;
        newVessel.GetComponent<uiShipScript>().shipButton = buttonShip.gameObject.GetComponent<Button>();
        buttonShip.gameObject.GetComponentInChildren<Text>().text = newVessel.name;
        selectedVessel = newVessel;
        Camera.main.gameObject.transform.position = new Vector3(newVessel.transform.position.x, Camera.main.gameObject.transform.position.y, newVessel.transform.position.z) ;
    }

    public static void dailyTrigger()
    {
        //Debug.Log("Daily Trigger was called at " + timeBehavior.gameDate.ToString());
        calculateShipUpkeep();
        
    }
    public static void weeklyTrigger()
    {
        //Debug.Log("Weekly Trigger was called at " + timeBehavior.gameDate.ToString());
    }
    public static void monthlyTrigger()
    {
        //Debug.Log("Monthly Trigger was called at " + timeBehavior.gameDate.ToString());
    }
    public static void yearlyTrigger()
    {
        //Debug.Log("Yearly Trigger was called at " + timeBehavior.gameDate.ToString());
    }

    public void Start() {
        vesselList.Add("Tanatside");
        vesselList.Add("The Cretan");
        vesselList.Add("Deux Amis");
        vesselList.Add("Aberdeen");
        vesselList.Add("Lantau");
        vesselList.Add("The Culverin");
        vesselList.Add("Happy Entrance");
        vesselList.Add("Kalgoorlie");
        vesselList.Add("Hawke");
        vesselList.Add("Paluma");
        vesselList.Add("Emulous");
        vesselList.Add("Flower de Luce");
        vesselList.Add("The Nighthawk");
        vesselList.Add("Deschaineux");
        vesselList.Add("Snapper");
        vesselList.Add("Marlborough");
        vesselList.Add("The Anne Royal");
        vesselList.Add("Gibraltar");
        vesselList.Add("Northumbria");
        vesselList.Add("The Hermes");
        vesselList.Add("Gabriel Harfleur");
        vesselList.Add("The Mistley");
        vesselList.Add("Nicator");
        vesselList.Add("Imperieuse");
        vesselList.Add("Alvington");
        vesselList.Add("Sterling");
        vesselList.Add("Blue Bottom");
        vesselList.Add("Sailors Wind");
        vesselList.Add("Banff");
        vesselList.Add("Dutiful");
        
    }


    
}
