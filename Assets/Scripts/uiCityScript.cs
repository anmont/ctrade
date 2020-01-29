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

    //store button prefab instance here so can update
    public List<tradeGoods> cityInventory = new List<tradeGoods>() { 
            new tradeGoods(){ productName="Grain", prefabName="ui/icons/Grain", tier=1, quantity=0 },
            new tradeGoods(){ productName="Timber", prefabName="ui/icons/Timber", tier=1, quantity=0 },
            new tradeGoods(){ productName="Fish", prefabName="ui/icons/Fish", tier=1, quantity=0 },
            new tradeGoods(){ productName="Silver", prefabName="ui/icons/Silver", tier=1, quantity=0 },
            new tradeGoods(){ productName="Iron", prefabName="ui/icons/Iron", tier=1, quantity=0 },
            new tradeGoods(){ productName="Copper", prefabName="ui/icons/Copper", tier=1, quantity=0 },
            new tradeGoods(){ productName="Tin", prefabName="ui/icons/Tin", tier=1, quantity=0 },
            new tradeGoods(){ productName="Spices", prefabName="ui/icons/Spices", tier=1, quantity=0 },
            new tradeGoods(){ productName="Perfumes", prefabName="ui/icons/Perfumes", tier=1, quantity=0 },
            new tradeGoods(){ productName="Gold", prefabName="ui/icons/Gold", tier=1, quantity=0 },
            new tradeGoods(){ productName="Jewels", prefabName="ui/icons/Jewels", tier=1, quantity=0 },
            new tradeGoods(){ productName="Leather Goods", prefabName="ui/icons/LeatherGoods", tier=1, quantity=0 },
            new tradeGoods(){ productName="Silk", prefabName="ui/icons/Silk", tier=1, quantity=0 },
            new tradeGoods(){ productName="Linen", prefabName="ui/icons/Linen", tier=1, quantity=0 },
            new tradeGoods(){ productName="Cotton", prefabName="ui/icons/Cotton", tier=1, quantity=0 },
            new tradeGoods(){ productName="Salt", prefabName="ui/icons/Salt", tier=1, quantity=0 },
            new tradeGoods(){ productName="Slaves", prefabName="ui/icons/Slaves", tier=1, quantity=0 }
        };
    // Start is called before the first frame update
    void Start()
    {
        thisCityScript = this.gameObject;
        //thisCity = this.transform.parent.gameObject;
        thisCity = this.gameObject;
        createButton();
        thisCityGuiButton.onClick.AddListener(navigateTo);
        // create button prefacb
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
            Debug.Log("Ship " + other.transform.gameObject.name + "has entered the city " + thisCity.name);
            foreach (MeshRenderer i in other.transform.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                i.enabled = false;//.enabled = true;
                other.transform.gameObject.GetComponent<uiShipScript>().location = thisCity.name;
            }
        
        //thisCity.name & other.transform.gameObject.name
        }
    }
    public void OnTriggerExit(Collider other) {
        if (other.transform.gameObject.tag == "Player")
        {
            Debug.Log("Ship " + other.transform.gameObject.name + "has exited the city " + thisCity.name);
            foreach (MeshRenderer i in other.transform.gameObject.GetComponentsInChildren<MeshRenderer>())
            {
                i.enabled = true;//.enabled = true;
                other.transform.gameObject.GetComponent<uiShipScript>().location = "At Sea";
            }
        }
    }
}
