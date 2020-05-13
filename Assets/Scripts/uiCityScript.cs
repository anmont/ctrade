using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class uiCityScript : MonoBehaviour
{
    public GameObject thisCityScript;
    public GameObject thisCity;
    public Button thisCityGuiButton;
    public int cityPopulation;
    public int productionMaterial1 = 50;
    public int productionMaterial2 = 50;
    public int productionMaterial3 = 50;
    public double cityProsperity;
    public double cityGrowth;
    public double cityWealth;
    public List<double> cityProsperityFloat = new List<double>();
    public List<double> cityGrowthFloat = new List<double>();
    public List<double> cityWealthFloat = new List<double>();


    
    //store button prefab instance here so can update
    public List<tradeGoods> cityInventory;
    // Start is called before the first frame update
    void Start()
    {
        thisCityScript = this.gameObject;
        //thisCity = this.transform.parent.gameObject;
        thisCity = this.gameObject;
        createButton();
        thisCityGuiButton.onClick.AddListener(navigateTo);
        // create button prefacb
        cityInventory = new List<tradeGoods>() { 
            new tradeGoods(){ productName="Grain", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Timber", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Fish", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Silver", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Iron", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Copper", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Tin", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Spices", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Perfumes", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Gold", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Jewels", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Leather Goods", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Silk", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Linen", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Cotton", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Clothes", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Salt", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 },
            new tradeGoods(){ productName="Slaves", perPersonPerYear=0.5, tier=1, quantity=500, cityBuyPrice=35, citySellPrice=49 }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (globals.cityList.Count > 9)
        {
            Text aAA = thisCityGuiButton.gameObject.GetComponentInChildren<Text>();
            string cityName = thisCity.gameObject.name.ToString();
            GameObject myShip = GameObject.FindGameObjectWithTag("Player");
            if (globals.selectedVessel == null)
            {
                aAA.text = cityName;
            }
            else
            {
                aAA.text = cityName + " (" + Mathf.RoundToInt(Vector3.Distance(thisCity.transform.position,globals.selectedVessel.transform.position)) + "km)";
            }
        }

    }

    public void createButton ()
    {
        GameObject buttonCity = (GameObject)Instantiate(Resources.Load("cityButton"));
        buttonCity.gameObject.transform.SetParent(GameObject.Find("guiCities").transform);
        buttonCity.gameObject.transform.localScale = new Vector3(1,1,1);
        thisCityGuiButton = buttonCity.GetComponent<Button>();
    }

    public void navigateTo()
    {
        if (globals.selectedVessel != null)
        {
            NavMeshAgent agent = globals.selectedVessel.GetComponent<NavMeshAgent>();
            agent.SetDestination(thisCity.transform.position);
        }
        
    }

    public void OnTriggerEnter(Collider other) {
        
        if (other.transform.gameObject.tag == "Player")
        {
            //Debug.Log("Ship " + other.transform.gameObject.name + "has entered the city " + thisCity.name);
            other.gameObject.GetComponent<uiShipScript>().cityAtAnchor = thisCity;
            //other.transform.gameObject.GetComponentInChildren<Projector>().enabled = false;
            other.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = false;

            foreach (MeshRenderer i in other.transform.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                i.enabled = false;
                other.transform.gameObject.GetComponent<uiShipScript>().location = thisCity.name;
            }
        
        //thisCity.name & other.transform.gameObject.name
        }
        if (other.transform.gameObject.tag == "ai")
        {
            //Debug.Log("Ship " + other.transform.gameObject.name + "has entered the city " + thisCity.name);
            other.gameObject.GetComponent<aiShipScript>().cityAtAnchor = thisCity;
            //other.transform.gameObject.GetComponentInChildren<Projector>().enabled = false;
            other.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = false;

            foreach (MeshRenderer i in other.transform.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                i.enabled = false;
                other.transform.gameObject.GetComponent<aiShipScript>().location = thisCity.name;
            }
        
        //thisCity.name & other.transform.gameObject.name
        }
    }
    public void OnTriggerExit(Collider other) {
        if (other.transform.gameObject.tag == "Player")
        {
            if (globals.selectedVessel == other.transform.gameObject)
            {
                //other.transform.gameObject.GetComponentInChildren<Projector>().enabled = true;
                other.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            Debug.Log("Ship " + other.transform.gameObject.name + "has exited the city " + thisCity.name);
            other.gameObject.GetComponent<uiShipScript>().cityAtAnchor = null;

            foreach (MeshRenderer i in other.transform.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                i.enabled = true;//.enabled = true;
                other.transform.gameObject.GetComponent<uiShipScript>().location = "At Sea";
            }
        }
         if (other.transform.gameObject.tag == "ai")
        {
            if (globals.selectedVessel == other.transform.gameObject)
            {
                //other.transform.gameObject.GetComponentInChildren<Projector>().enabled = true;
                other.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            Debug.Log("Ship " + other.transform.gameObject.name + "has exited the city " + thisCity.name);
            other.gameObject.GetComponent<aiShipScript>().cityAtAnchor = null;

            foreach (MeshRenderer i in other.transform.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                i.enabled = true;//.enabled = true;
                other.transform.gameObject.GetComponent<aiShipScript>().location = "At Sea";
            }
        }
    }
}
