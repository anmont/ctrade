using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globals : MonoBehaviour
{

    public GameObject shipParentPanel;
    public static GameObject escapeMenu;
    public static GameObject selectedVessel;
    public static GameObject time;
    public static GameObject navMesh;
    public static List<GameObject> cityList = new List<GameObject>();
    public static List<GameObject> shipList = new List<GameObject>();
    public static List<string> vesselList = new List<string>();

    public static void createVessel(Vector3 location)
    {
        //innstantiate
        GameObject newVessel = (GameObject)Instantiate(Resources.Load("ship"), location, Quaternion.identity);

        //Name the vessel
        int randMe = Random.Range(0,vesselList.Count);
        newVessel.name = vesselList[randMe];
        vesselList.RemoveAt(randMe);

        //update vessel list
        shipList.Add(newVessel);
        //GameObject buttonShip = null;

        //update GUI button for the vessel
        GameObject buttonShip = (GameObject)Instantiate(Resources.Load("shipButton"));
        buttonShip.gameObject.transform.SetParent(GameObject.Find("guiVessels").transform);
        buttonShip.gameObject.transform.localScale = new Vector3(1,1,1);
        //buttonShip.gameObject.transform.parent = ; 
        buttonShip.GetComponent<uiShipScript>().thisVessel = newVessel.gameObject;
        buttonShip.gameObject.GetComponentInChildren<Text>().text = newVessel.name;
        selectedVessel = newVessel;
        Camera.main.gameObject.transform.position = new Vector3(newVessel.transform.position.x, Camera.main.gameObject.transform.position.y, newVessel.transform.position.z) ;
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
