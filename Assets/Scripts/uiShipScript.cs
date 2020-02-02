using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiShipScript : MonoBehaviour
{
    
    public GameObject vesselInstance;
    public string location = "At Sea";
    public int vesselStorageSize;
    public int vesselClassification;
    public Button shipButton;

    public List<tradeGoods> shipInventory = new List<tradeGoods>() { 
            new tradeGoods(){ productName="Grain", prefabName="ui/icons/Grain", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Timber", prefabName="ui/icons/Timber", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Fish", prefabName="ui/icons/Fish", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Silver", prefabName="ui/icons/Silver", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Iron", prefabName="ui/icons/Iron", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Copper", prefabName="ui/icons/Copper", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Tin", prefabName="ui/icons/Tin", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Spices", prefabName="ui/icons/Spices", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Perfumes", prefabName="ui/icons/Perfumes", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Gold", prefabName="ui/icons/Gold", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Jewels", prefabName="ui/icons/Jewels", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Leather Goods", prefabName="ui/icons/LeatherGoods", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Silk", prefabName="ui/icons/Silk", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Linen", prefabName="ui/icons/Linen", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Cotton", prefabName="ui/icons/Cotton", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Clothes", prefabName="ui/icons/Cotton", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Salt", prefabName="ui/icons/Salt", perPersonPerYear=0.5, tier=1, quantity=10 },
            new tradeGoods(){ productName="Slaves", prefabName="ui/icons/Slaves", perPersonPerYear=0.5, tier=1, quantity=10 }
        };
        
    public GameObject thisVessel;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("what is this");
        //vesselInstance = this.gameObject.paren;
        //shipButton = this.gameObject.GetComponent<Button>();
        shipButton.onClick.AddListener(selectThisVessel);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void selectThisVessel()
    {
        if (globals.selectedVessel != null)
        {
            //disable the selected ring
            globals.selectedVessel.GetComponentInChildren<Projector>().enabled = false;
        }


        globals.selectedVessel = thisVessel;

        //enable selection ring of this vessel
        if (globals.selectedVessel.gameObject.GetComponent<uiShipScript>().location == "At Sea")
        {
            globals.selectedVessel.GetComponentInChildren<Projector>().enabled = true;
        }

        Camera.main.gameObject.transform.position = new Vector3(thisVessel.transform.position.x, Camera.main.gameObject.transform.position.y, thisVessel.transform.position.z) ;

    }
}
