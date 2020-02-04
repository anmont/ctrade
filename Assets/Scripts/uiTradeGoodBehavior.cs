using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class uiTradeGoodBehavior : MonoBehaviour
{
    
    public int index;
    public GameObject thisObject;
    public GameObject thisParentObject;
    // Start is called before the first frame update
    void Start()
    {
        thisObject = this.gameObject;
        thisParentObject = thisObject.transform.parent.parent.parent.parent.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeTrade()
    {
        GameObject city = thisParentObject.GetComponent<cityTradePanel>().cityToTrade;
        GameObject ship = thisParentObject.GetComponent<cityTradePanel>().vesselToTrade;
        tradeNow(city,ship,index,thisObject.transform.GetComponentInChildren<Slider>().value);

    }

    public void tradeNow(GameObject city, GameObject ship, int item, float qty)
    {
        //sell means ship looses qty
        ship.GetComponent<uiShipScript>().shipInventory[item].quantity += qty;
        city.GetComponent<uiCityScript>().cityInventory[item].quantity += (qty * -1);

        //update slider to zero and forcce the update of the values on the next frame
        thisObject.transform.GetComponentInChildren<Slider>().value = 0;
        thisParentObject.GetComponent<cityTradePanel>().lastUpdateDay = 52;
    }

    public void getUpdate()
    {
        Text[] arr = thisObject.transform.GetComponentsInChildren<Text>();
        
        arr[0].text = thisObject.GetComponentInChildren<Slider>().value.ToString();
    }
}
