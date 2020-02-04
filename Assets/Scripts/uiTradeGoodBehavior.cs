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
        

        if (qty > 0)
        {
            //here I need to check the money... if insufficient rollback action
            //playercash - (qty * unit buy price)
            double newCashValue = globals.playerCash - (qty * city.GetComponent<uiCityScript>().cityInventory[item].cityBuyPrice);
            if (newCashValue > 0)
            {
                //sell means ship looses qty
                ship.GetComponent<uiShipScript>().shipInventory[item].quantity += qty;
                city.GetComponent<uiCityScript>().cityInventory[item].quantity += (qty * -1);
                globals.playerCash = newCashValue;
            }
        }
        else
        {
            ship.GetComponent<uiShipScript>().shipInventory[item].quantity += qty;
            city.GetComponent<uiCityScript>().cityInventory[item].quantity += (qty * -1);
            globals.playerCash += ((qty * -1) * city.GetComponent<uiCityScript>().cityInventory[item].citySellPrice);
        }

        //update slider to zero and force the update of the values on the next frame
        thisObject.transform.GetComponentInChildren<Slider>().value = 0;
        thisParentObject.GetComponent<cityTradePanel>().lastUpdateDay = 52;


    }

    public void getUpdate()
    {
        Text[] arr = thisObject.transform.GetComponentsInChildren<Text>();
        
        arr[0].text = thisObject.GetComponentInChildren<Slider>().value.ToString();

        double qty = thisObject.GetComponentInChildren<Slider>().value;
        GameObject city = thisParentObject.GetComponent<cityTradePanel>().cityToTrade;
        //determine calculations and show value
        double calcValue = 0;
        if (qty > 0)
        {
            //here I need to check the money... if insufficient rollback action
            //playercash - (qty * unit buy price)
            calcValue = ((qty * -1) * city.GetComponent<uiCityScript>().cityInventory[index].cityBuyPrice);
        }
        else
        {
            calcValue = ((qty * -1) * city.GetComponent<uiCityScript>().cityInventory[index].citySellPrice);
        }

        arr[2].text = "¢" + calcValue; //Total amount of the trade
    }
}
