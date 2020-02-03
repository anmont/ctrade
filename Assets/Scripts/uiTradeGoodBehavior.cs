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
        Debug.Log("Make the trade now");
        Debug.Log(thisObject.transform.parent.parent.parent.parent.parent.gameObject.name);

    }

    public void tradeNow(GameObject city, GameObject ship, int item, float qty)
    {
        from.GetComponent<city
    }

    public void getUpdate()
    {
        Text[] arr = thisObject.transform.GetComponentsInChildren<Text>();
        
        arr[0].text = thisObject.GetComponentInChildren<Slider>().value.ToString();
    }
}
