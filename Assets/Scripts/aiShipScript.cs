using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class aiShipScript : MonoBehaviour
{
    
    //public GameObject vesselInstance;
    public string location = "At Sea";
    public int jobNumber = 0;
    public int jobState = 0;
    //0 == not assigned
    //1 == assigned and on route
    //2 == arrived
    //3 == picked up goods, heading to dest
    //4 == arrived
    //5 == goods delivered > back to 0
    public int vesselStorageSize;
    public int vesselClassification;
    //public Button shipButton;
    public GameObject cityAtAnchor;

    public List<tradeGoods> shipInventory;
        
    public GameObject thisVessel;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("what is this");
        //vesselInstance = this.gameObject.paren;
        //shipButton = this.gameObject.GetComponent<Button>();
        //shipButton.onClick.AddListener(selectThisVessel);
        thisVessel = this.gameObject;

        if (shipInventory.Count > 0)
        {
            //
        }
        else
        {
            shipInventory = new List<tradeGoods>() { 
                new tradeGoods(){ productName="Grain", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Timber", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Fish", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Silver", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Iron", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Copper", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Tin", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Spices", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Perfumes", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Gold", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Jewels", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Leather Goods", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Silk", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Linen", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Cotton", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Clothes", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Salt", perPersonPerYear=0.5, tier=1, quantity=0 },
                new tradeGoods(){ productName="Slaves", perPersonPerYear=0.5, tier=1, quantity=0 }
            };
        }
        
    }

    private aiTradeJobs getJobById(int id)
    {
        aiTradeJobs ret = globals.aiTradJobList.Find(item => item.id == id);

        return ret;

    }

    // Update is called once per frame
    void Update()
    {
            //0 == not assigned
            //1 == assigned and on route to source city
            //2 == arrived at source
            //3 == picked up goods, heading to dest
            //4 == arrived
            //5 == goods delivered > back to 0
        if (cityAtAnchor != null)
        {
            if (jobState == 0)
            {
                foreach (aiTradeJobs jobs in globals.aiTradJobList)
                {
                    if (!jobs.assigned)
                    {
                        jobs.assigned = true;
                        jobNumber = jobs.id;

                        NavMeshAgent agent = thisVessel.GetComponent<NavMeshAgent>();
                        agent.SetDestination(jobs.aiSourceCity.transform.position);

                        jobState = 1;
                        break;
                    }
                    
                }
                //then i need an assignment 
                // once assigned and routed then assign to 1
            }
            if (jobState == 1)
            {
                aiTradeJobs thisJob = getJobById(jobNumber);
                //then check if we are at the source city and transition to job state 2
                // if not sleep x times, and reroute to  the correct city
                if (cityAtAnchor == thisJob.aiSourceCity)
                {
                    jobState = 2;
                }

            }
            if (jobState == 2)
            {
                aiTradeJobs thisJob = getJobById(jobNumber);
                //remove the correct amount of goods from the city and put them in the vessel

                thisJob.aiSourceCity.GetComponent<uiCityScript>().cityInventory[thisJob.tradeGoodIndex].quantity -= 200;
                thisVessel.gameObject.GetComponent<aiShipScript>().shipInventory[thisJob.tradeGoodIndex].quantity += 200;
                //head to destination 
                NavMeshAgent agent = thisVessel.GetComponent<NavMeshAgent>();
                agent.SetDestination(thisJob.aiDestCity.transform.position);
            }
            if (jobState == 3)
            {
                aiTradeJobs thisJob = getJobById(jobNumber);
                //if dest city is reached transition to 4
                if (cityAtAnchor == thisJob.aiDestCity)
                {
                    jobState = 4;
                }
            }
            if (jobState == 4)
            {
                aiTradeJobs thisJob = getJobById(jobNumber);
                //deliver goods from vessel to goods in the city
                thisVessel.gameObject.GetComponent<aiShipScript>().shipInventory[thisJob.tradeGoodIndex].quantity -= 200;
                thisJob.aiDestCity.GetComponent<uiCityScript>().cityInventory[thisJob.tradeGoodIndex].quantity += 200;
                
                //set to 5
                jobState = 5;
            }
            if (jobState == 5)
            {
                aiTradeJobs thisJob = getJobById(jobNumber);
                globals.aiTradJobList.RemoveAt(globals.aiTradJobList.FindIndex(item => item.id == thisJob.id));
                jobState = 0;
                //set state to 0
            }
        }

    }

    public void selectThisVessel()
    {
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
            if (globals.selectedVessel.gameObject.GetComponent<aiShipScript>().location == "At Sea")
            {
                globals.selectedVessel.transform.Find("selector").gameObject.GetComponent<MeshRenderer>().enabled = true;
                //globals.selectedVessel.GetComponentInChildren<Projector>().enabled = true;
            }

            Camera.main.gameObject.transform.position = new Vector3(thisVessel.transform.position.x, Camera.main.gameObject.transform.position.y, thisVessel.transform.position.z) ;
        }

    }
}
