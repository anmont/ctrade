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
