using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiShipScript : MonoBehaviour
{
    
    //public GameObject vesselInstance;
    public string location = "At Sea";
    public int vesselStorageSize;
    public int vesselClassification;
    public Transform myCamera;
    public Camera mainCamera;
    public Button shipButton;
    public GameObject cityAtAnchor;

    public List<tradeGoods> shipInventory;
        
    public GameObject thisVessel;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("what is this");
        //vesselInstance = this.gameObject.paren;
        //shipButton = this.gameObject.GetComponent<Button>();
        
        shipButton.onClick.AddListener(selectThisVessel);

        if (shipInventory.Count > 0)
        {
            //
        }
        else
        {
            shipInventory = new List<tradeGoods>() { 
                new tradeGoods(){ productName="Grain", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Timber", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Fish", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Silver", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Iron", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Copper", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Tin", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Spices", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Perfumes", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Gold", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Jewels", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Leather Goods", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Silk", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Linen", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Cotton", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Clothes", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Salt", perPersonPerYear=0.5, tier=1, quantity=5 },
                new tradeGoods(){ productName="Slaves", perPersonPerYear=0.5, tier=1, quantity=5 }
            };
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (globals.selectedVessel != null)
        {
            if (globals.selectedVessel == thisVessel)
            {
                globals.selectedVessel.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = true;
                //globals.selectedVessel.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = false;
                //globals.selectedVessel = thisVessel;
            }
            else
            {
                globals.selectedVessel.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        */
    }

    public void selectThisVessel()
    {
        if (mainCamera == null)
        {
            myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
            mainCamera = myCamera.GetComponent<Camera>();
        }

        if (globals.selectedVessel != thisVessel)
        {
            if (globals.selectedVessel != null)
            {
                //disable the selected ring
                globals.selectedVessel.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = false;
                
                //globals.selectedVessel.GetComponentInChildren<Projector>().enabled = false;
            }


            globals.selectedVessel = thisVessel;

            //enable selection ring of this vessel
            if (globals.selectedVessel.gameObject.GetComponent<uiShipScript>().location == "At Sea")
            {
                globals.selectedVessel.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = true;
                //globals.selectedVessel.GetComponentInChildren<Projector>().enabled = true;
            }

            
        }
        mainCamera.transform.position = new Vector3(thisVessel.transform.position.x, mainCamera.transform.position.y, thisVessel.transform.position.z) ;

    }
}
