﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class globals : MonoBehaviour
{

    public GameObject shipParentPanel;
    public static double playerCash = 100000;
    public static saveScript saveInstance;
    public static GameObject escapeMenu;
    public static Vector3 mapSeed;
    public static GameObject cityEconomyInstance;
    public static GameObject selectedVessel;
    public static GameObject time;
    public static GameObject navMesh;
    public static GameObject shipGroup;
    public static GameObject aiGroup;
    public static GameObject cityGroup;
    public static int aiVessels = 24;
    public static List<aiTradeJobs> aiTradJobList = new List<aiTradeJobs>();
    public static List<shipAssignments> aiShipAssignmentList = new List<shipAssignments>();
    public static List<GameObject> cityList = new List<GameObject>();
    public static List<GameObject> shipList = new List<GameObject>();
    public static List<GameObject> aiShipList = new List<GameObject>();
    public static List<playerTradeRoutes> playerTradeRouteList = new List<playerTradeRoutes>();
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


    public static void openCityProperties(GameObject city)
    {
        GameObject cityPropI = (GameObject)Instantiate(Resources.Load("cityDetails"));
        cityPropI.transform.SetParent(GameObject.Find("mainCanvas").transform);
        cityPropI.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f,0f);
        cityPropI.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        cityPropI.GetComponentInChildren<ScrollRect>().scrollSensitivity = 50f;
        //shipDetailI.GetComponent<cityTradePanel>().cityToTrade = city;
        cityPropI.GetComponent<cityDetailsPanel>().cityDetail = city;
        //timeBehavior.lastTS = timeBehavior.currentTimeScale; 
        //timeBehavior.changeTimeScale(0);
    }



    public static void openShipDetails(GameObject ship)
    {
        GameObject shipDetailI = (GameObject)Instantiate(Resources.Load("shipDetails"));
        shipDetailI.transform.SetParent(GameObject.Find("mainCanvas").transform);
        shipDetailI.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f,0f);
        shipDetailI.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        shipDetailI.GetComponentInChildren<ScrollRect>().scrollSensitivity = 50f;
        //shipDetailI.GetComponent<cityTradePanel>().cityToTrade = city;
        shipDetailI.GetComponent<shipDetailsPanel>().vesselToTrade = ship;
        //timeBehavior.lastTS = timeBehavior.currentTimeScale; 
        //timeBehavior.changeTimeScale(0);
    }

    public static float navMeshDistance(Vector3 PointA, Vector3 PointB)
    {
        float retDistance = 0f;
        if (PointA == null || PointB == null)
        {
            return retDistance;
        }

        NavMesh.SamplePosition(PointA, out NavMeshHit hitA, 10f, NavMesh.AllAreas);
        NavMesh.SamplePosition(PointB, out NavMeshHit hitB, 10f, NavMesh.AllAreas);
 
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas, path))
        {
            //Debug.DrawLine(hitA.position + Vector3.up, hitB.position + Vector3.up, Color.red, 10f, true); // a red line float in air
            int cnt = path.corners.Length;
           
            float distance = 0f;
            for (int i =0; i<cnt - 1; i++)
            {
                distance += (path.corners[i] - path.corners[i + 1]).magnitude;
                //Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.green, 10f, true);
            }
            //Debug.Log($"Total distance {distance:F2}");
            retDistance = distance;
        }
        else
        {
            //Debug.LogError("Mission Fail");
        }
        return retDistance;
    }

    public static void openTradeWindow(GameObject city, GameObject ship)
    {
        GameObject shipTradePanelI = (GameObject)Instantiate(Resources.Load("shipTradePanel"));
        shipTradePanelI.transform.SetParent(GameObject.Find("mainCanvas").transform);
        shipTradePanelI.GetComponent<RectTransform>().localPosition = new Vector3(0f,0f,0f);
        shipTradePanelI.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        shipTradePanelI.GetComponentInChildren<ScrollRect>().scrollSensitivity = 50f;
        shipTradePanelI.GetComponent<cityTradePanel>().cityToTrade = city;
        shipTradePanelI.GetComponent<cityTradePanel>().vesselToTrade = ship;
        timeBehavior.lastTS = timeBehavior.currentTimeScale; 
        timeBehavior.changeTimeScale(0);
    }
    public static GameObject createCity(Vector3 location, string load = "n")
    {
        Vector3 landHitLoc = Vector3.zero;
        RaycastHit hit;
        //normalize height
        Physics.Raycast(new Vector3(location.x, 100 , location.z), Vector3.down, out hit , 150f);
        landHitLoc = hit.point;
        landHitLoc.y = 1.5f;

        //create gameobject
        GameObject newCity = (GameObject)Instantiate(Resources.Load("City"),landHitLoc,Quaternion.identity);
        newCity.transform.SetParent(cityGroup.transform,false);
        newCity.transform.position = landHitLoc;

        
        //give the city the object reference TODO remove this and have the start script update this info
        newCity.GetComponent<uiCityScript>().thisCity = newCity;

        if (load == "y")
        {

        }
        else
        {
            //get a random name from the list
            int randMe = Random.Range(0,terrainGen.cityList.Count);
            newCity.name = terrainGen.cityList[randMe];
            terrainGen.cityList.RemoveAt(randMe);
            //give the city production assignment
            globals.cityEconomyInstance.gameObject.GetComponent<cityEconomy>().determineProduction(newCity);

            //give the city a population
            globals.cityEconomyInstance.gameObject.GetComponent<cityEconomy>().determinePopulation(newCity);
        }

        //Add the city to the city list
        globals.cityList.Add(newCity);

        //TODO Determine what the starting population of the city should be
        //TODO Determine what products the city should produce

        return newCity;
    }

    public static GameObject createAiVessel(Vector3 location, string shipName = "none")
    {
        string prefabName = "aiShip";


        //deal with globals loading before scene
        if (cityGroup == null)
        {
            cityGroup = GameObject.Find("cityGroup").gameObject;
            shipGroup = GameObject.Find("shipGroup").gameObject;
            aiGroup = GameObject.Find("aiGroup").gameObject;
        }

        //instantiate
        GameObject newVessel = (GameObject)Instantiate(Resources.Load(prefabName), location, Quaternion.identity);
        newVessel.transform.SetParent(aiGroup.transform, false);

        //Name the vessel
        if (shipName == "none")
        {
            int randMe = Random.Range(0,vesselList.Count);
            newVessel.name = vesselList[randMe];
            vesselList.RemoveAt(randMe);
        }
        else
        {
            newVessel.name = shipName;
        }

        //update vessel list
        aiShipList.Add(newVessel);
        //GameObject buttonShip = null;

        //update GUI button for the vessel
        //GameObject buttonShip = (GameObject)Instantiate(Resources.Load("shipButton"));
        //buttonShip.GetComponent<btnShip>().thisVessel = newVessel;
        //Button testB = Instantiate(Resources.Load("shipButton"));
        //buttonShip.gameObject.transform.SetParent(GameObject.Find("guiVessels").transform);
        //buttonShip.gameObject.transform.localScale = new Vector3(1,1,1);
        //buttonShip.gameObject.transform.parent = ; 
        newVessel.GetComponent<aiShipScript>().thisVessel = newVessel.gameObject;
        //newVessel.GetComponent<aiShipScript>().shipButton = buttonShip.gameObject.GetComponent<Button>();
        //buttonShip.gameObject.GetComponentInChildren<Text>().text = newVessel.name;
        //selectedVessel = newVessel;
        //newVessel.gameObject.GetComponent<uiShipScript>().selectThisVessel();
        //Camera.main.gameObject.transform.position = new Vector3(newVessel.transform.position.x, Camera.main.gameObject.transform.position.y, newVessel.transform.position.z) ;
        newVessel.transform.position = location;
        
        return newVessel;
    }

    public static GameObject createVessel(Vector3 location, int size, string shipName = "none")
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

        //deal with globals loading before scene
        if (cityGroup == null)
        {
            cityGroup = GameObject.Find("cityGroup").gameObject;
            shipGroup = GameObject.Find("shipGroup").gameObject;
            aiGroup = GameObject.Find("aiGroup").gameObject;
        }

        //innstantiate
        GameObject newVessel = (GameObject)Instantiate(Resources.Load(prefabName), location, Quaternion.identity);
        newVessel.transform.SetParent(shipGroup.transform, false);

        //Name the vessel
        if (shipName == "none")
        {
            int randMe = Random.Range(0,vesselList.Count);
            newVessel.name = vesselList[randMe];
            vesselList.RemoveAt(randMe);
        }
        else
        {
            newVessel.name = shipName;
        }

        //update vessel list
        shipList.Add(newVessel);
        //GameObject buttonShip = null;

        //update GUI button for the vessel
        GameObject buttonShip = (GameObject)Instantiate(Resources.Load("shipButton"));
        buttonShip.GetComponent<btnShip>().thisVessel = newVessel;
        //Button testB = Instantiate(Resources.Load("shipButton"));
        buttonShip.gameObject.transform.SetParent(GameObject.Find("guiVessels").transform);
        buttonShip.gameObject.transform.localScale = new Vector3(1,1,1);
        //buttonShip.gameObject.transform.parent = ; 
        newVessel.GetComponent<uiShipScript>().thisVessel = newVessel.gameObject;
        newVessel.GetComponent<uiShipScript>().shipButton = buttonShip.gameObject.GetComponent<Button>();
        buttonShip.gameObject.GetComponentInChildren<Text>().text = newVessel.name;
        //selectedVessel = newVessel;
        newVessel.gameObject.GetComponent<uiShipScript>().selectThisVessel();
        Camera.main.gameObject.transform.position = new Vector3(newVessel.transform.position.x, Camera.main.gameObject.transform.position.y, newVessel.transform.position.z) ;
        newVessel.transform.position = location;
        
        return newVessel;
    }


    public static void dailyTrigger()
    {
        //Debug.Log("Daily Trigger was called at " + timeBehavior.gameDate.ToString());
        // TODO create coroutines for each
        calculateShipUpkeep();
        //Calculate City Growth


        //Debug.Log("Terrain location = " + GameObject.Find("Terrain").transform.position.ToString());
        //Debug.Log("Water location = " + GameObject.Find("Water").transform.position.ToString());
        //Debug.Log("City 0 location = " + cityList[0].transform.position.ToString());
        //Debug.Log("Ship 0 location = " + shipList[0].transform.position.ToString());


        //Calculate City Production
        globals.cityEconomyInstance.gameObject.GetComponent<cityEconomy>().calculateProduction();

        //Calculate CIty Consumption
        globals.cityEconomyInstance.gameObject.GetComponent<cityEconomy>().calculateConsumption();

        //calculate growth
        globals.cityEconomyInstance.gameObject.GetComponent<cityEconomy>().calculateGrowth();
        //globals.openTradeWindow(globals.cityList[0].gameObject,globals.shipList[0].gameObject);
        
    }
    public static void everyOtherDayTrigger()
    {
        //Debug.Log("Every Other Day Triggered");
        globals.cityEconomyInstance.gameObject.GetComponent<cityEconomy>().determineCityJobs();
        
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
        playerTradeRouteList.Add(new playerTradeRoutes(){ tradeRouteId = 0, tradeRouteName = "test0"});
        playerTradeRouteList.Add(new playerTradeRoutes(){ tradeRouteId = 1, tradeRouteName = "test1"});
        playerTradeRouteList.Add(new playerTradeRoutes(){ tradeRouteId = 2, tradeRouteName = "test2"});
        playerTradeRouteList.Add(new playerTradeRoutes(){ tradeRouteId = 3, tradeRouteName = "test3"});

        
        
        cityGroup = GameObject.Find("cityGroup").gameObject;
        shipGroup = GameObject.Find("shipGroup").gameObject;
        aiGroup = GameObject.Find("aiGroup").gameObject;

    }
}

public class shipAssignments
{
    public float assignmentID;
    public bool assigned;
    public GameObject assignedVessel;
    public int productID;
    public GameObject sourceCity;
    public GameObject destinationCity;

}

public class aiTradeJobs
{
    public string id;
    // city index - TradeGood Id 0101
    // CC - TT
    // city 1 tradegood 2 = 0102
    public int tradeGoodIndex;
    public int tradeQty;
    public bool assigned;
    public GameObject aiSourceCity;
    public GameObject aiDestCity;

}

public class playerTradeRoutes
{
    
    public float tradeRouteId;
    public string tradeRouteName;
    public List<routeTradeJobs> jobList = new List<routeTradeJobs>();

}

public class routeTradeJobs
{
    //public int jobIndex;
    public GameObject location;
    public int tradeAction; // 0 load | 1 buy | 2 sell | 3 store
    public int tradeItemIndex;
    public int tradeQty;
    public int buySellValue;
    public bool waitForCompletion;

}
