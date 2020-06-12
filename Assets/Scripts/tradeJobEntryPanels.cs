using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class tradeJobEntryPanels : MonoBehaviour
{
    public Vector2 lastMousePosition;
    public float currentRouteID;
    public int currentJobIndex = -1;
    //public GameObject cityToTrade;
    public GameObject contentGO;
    public GameObject myInstance;
    public Dropdown myCityList;
    public Dropdown myActionList;
    public Dropdown myItemList;
    public int lastUpdateDay = 44;
    public string currentlySelected = "0";


    public void onSelect (Transform who) 
    {
        //Debug.Log("on click presed");
        //Debug.Log(who.name + " was clicked");
    }

    public void saveJobToRoute(string city, int action, int item, int quantity, int buySellValues, bool waitForCompletions)
    {
        playerTradeRoutes route = globals.playerTradeRouteList[globals.playerTradeRouteList.FindIndex(a => a.tradeRouteId == currentRouteID)];

        if (currentJobIndex == -1)
        {
            //add the record new
            route.jobList.Add(new routeTradeJobs() { 
                location = globals.cityList[globals.cityList.FindIndex( a => a.name == city)], 
                tradeAction = action, 
                tradeItemIndex = item, 
                tradeQty = quantity, 
                buySellValue = buySellValues, 
                waitForCompletion = waitForCompletions 
                });

        }
        else
        {
            Debug.Log("I am updating a previous entry");
            //edit the previous entry
            route.jobList[currentJobIndex].location = globals.cityList[globals.cityList.FindIndex( a => a.name == city)];
            route.jobList[currentJobIndex].tradeAction = action;
            route.jobList[currentJobIndex].tradeItemIndex = item;
            route.jobList[currentJobIndex].tradeQty = quantity;
            route.jobList[currentJobIndex].buySellValue = buySellValues;
            route.jobList[currentJobIndex].waitForCompletion = waitForCompletions;
        }

    }


    public void saveClick ()
    {
        //get the route
        
        //float uID = Time.realtimeSinceStartup;
        //globals.playerTradeRouteList.Add(new playerTradeRoutes(){ tradeRouteId = uID, tradeRouteName = "New Trade Route"});
        // playerTradeRouteList.Add(new playerTradeRoutes(){ tradeRouteId = 0, tradeRouteName = "test0"});
        //Debug.Log("item " + myItemList.options[myItemList.value].text);
        //Debug.Log("city " + myCityList.options[myCityList.value].text);
        //Debug.Log("action " + myActionList.options[myActionList.value].text);
        //Debug.Log("quantity " + myInstance.transform.Find("inpQy").Find("Text").GetComponent<Text>().text);
        //Debug.Log("buysell " + myInstance.transform.Find("inpBuySellAmount").Find("Text").GetComponent<Text>().text);
        //Debug.Log("wait " + myInstance.transform.Find("Toggle").GetComponent<Toggle>().isOn.ToString());

        //add the new item to the route
        saveJobToRoute(myCityList.options[myCityList.value].text, (myActionList.value - 1), (myItemList.value - 1), int.Parse(myInstance.transform.Find("inpQy").Find("Text").GetComponent<Text>().text), int.Parse(myInstance.transform.Find("inpBuySellAmount").Find("Text").GetComponent<Text>().text), myInstance.transform.Find("Toggle").GetComponent<Toggle>().isOn);

        //route.jobList.Add(new routeTradeJobs() {});
        Debug.Log("save was clicked for index " + currentlySelected.ToString());
        GameObject.Find("tradeRouteDetails(Clone)").GetComponent<tradeRouteDetailsPanel>().refresh();
        Destroy(myInstance);
        //refresh();
    }

    
    public void edit(int index)
    {
        


        //destroy children
        //Transform[] children = contentGUI.GetChildCount.GetComponentsInChildren<Transform>();
        //int cnt = contentGUI.childCount;
        //int i = 0;
        //while (i < cnt)
        //{
        //    Destroy(contentGUI.gameObject.transform.GetChild(0));
        //    i++;
        //}

        //foreach (Transform item in children)
        //{
        //    Destroy(item);
        //}
        //playerTradeRoutes route = globals.playerTradeRouteList[globals.playerTradeRouteList.FindIndex(a => a.tradeRouteId == currentRouteID)];
        //myroute = globals.playerTradeRouteList[globals.playerTradeRouteList.FindIndex(a => a.tradeRouteId == currentRouteID)];

        //add children


        //Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        //GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array


        /*Text[] uiTexts = this.GetComponentsInChildren<Text>();

        uiTexts[4].transform.parent.GetComponent<InputField>().text = cityDetail.name;
        uiTexts[6].text = cityDetail.GetComponent<uiCityScript>().cityPopulation.ToString();
        uiTexts[8].text = cityDetail.GetComponent<uiCityScript>().cityProsperity.ToString();
        //uiTexts[10].text = cityDetail.GetComponent<uiCityScript>().cityGrowth.ToString();
        uiTexts[10].text = cityDetail.GetComponent<uiCityScript>().cityGrowth.ToString();
        //uiTexts[12].text = cityDetail.GetComponent<uiCityScript>().cityWealth.ToString();
        uiTexts[12].text = cityDetail.GetComponent<uiCityScript>().cityWealth.ToString();
        uiTexts[16].text = cityDetail.GetComponent<uiCityScript>().cityInventory[cityDetail.GetComponent<uiCityScript>().productionMaterial1].productName;
        uiTexts[17].text = cityDetail.GetComponent<uiCityScript>().cityInventory[cityDetail.GetComponent<uiCityScript>().productionMaterial2].productName;
        uiTexts[18].text = cityDetail.GetComponent<uiCityScript>().cityInventory[cityDetail.GetComponent<uiCityScript>().productionMaterial3].productName;
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        myInstance = this.gameObject;

        //this.gameObject.GetComponent<panel..onClick.AddListener(navigateTo);
        //EventTrigger triggerA = GetComponent<EventTrigger>();
        myInstance.GetComponentsInChildren<Button>()[0].onClick.AddListener(onExitClick);
        EventTrigger trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entrype = new EventTrigger.Entry();
        entrype.eventID = EventTriggerType.PointerEnter;
        entrype.callback.AddListener((data) => { onPointerEnter(); });
        trigger.triggers.Add(entrype);

        EventTrigger.Entry entrypx = new EventTrigger.Entry();
        entrypx.eventID = EventTriggerType.PointerExit;
        entrypx.callback.AddListener((data) => { onPointerExit(); });
        trigger.triggers.Add(entrypx);

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drag;
        entry.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.BeginDrag;
        entry.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
        trigger.triggers.Add(entry);
        //trigger.triggers.Add(entry);


        //uiTexts[3].text = cityToTrade.name;
        //uiTexts[6].text = vesselToTrade.name;
        //Transform temp = myInstance.transform.Find("shipTradeCityName");
        //myInstance.transform.Find("shipTradeCityName").GetComponent<Text>().text = cityToTrade.name;
        //myInstance.transform.Find("shipTradeShipName").GetComponent<Text>().text = vesselToTrade.name;


            //uiTexts[6].text = cityDetail.GetComponent<uiCShipScript>().vesselStorageSize.ToString();
            //float totalInv = 0;
            //foreach (tradeGoods good in cityDetail.GetComponent<uiCityScript>().cityInventory)
            //{
            //    totalInv += (float)good.quantity;
            //}

            //uiTexts[8].text = totalInv.ToString();
            //uiTexts[10].text = (vesselToTrade.GetComponent<uiShipScript>().vesselStorageSize - totalInv).ToString();
            //uiTexts[12].text = vesselToTrade.GetComponent<uiShipScript>().location;
            //uiTexts[14].text = "Not Assigned"; //TODO
//clear/remove all option item


        

        //Trigger getting the list of cities
        myCityList.options.Clear();
        myCityList.options.Add(new Dropdown.OptionData() {text="---"});
        foreach (GameObject city in globals.cityList)
        {
            myCityList.options.Add(new Dropdown.OptionData() {text=city.name});
        }

        //Trigger getting the list of items
        myItemList.options.Clear();
        myItemList.options.Add(new Dropdown.OptionData() {text="---"});
        foreach (tradeGoods item in globals.cityList[0].GetComponent<uiCityScript>().cityInventory)
        {
            myItemList.options.Add(new Dropdown.OptionData() {text=item.productName});
        }

        if (currentJobIndex > -1)
        {
            //.transform.Find("txtTradeRouteJobTitle").GetComponent<Text>().text = "Trade Route Job - Edit";
            //currentJobIndex = index;
            playerTradeRoutes route = globals.playerTradeRouteList[globals.playerTradeRouteList.FindIndex(a => a.tradeRouteId == currentRouteID)];
            //Debug.Log(route.jobList[currentJobIndex].location.name);
            //Debug.Log(myCityList.options.FindIndex( a => a.text == route.jobList[currentJobIndex].location.name).ToString());
            myCityList.value = myCityList.options.FindIndex( a => a.text == route.jobList[currentJobIndex].location.name);
            myActionList.value = route.jobList[currentJobIndex].tradeAction + 1;
            myItemList.value = route.jobList[currentJobIndex].tradeItemIndex + 1;
            myInstance.transform.Find("inpQy").GetComponent<InputField>().text = route.jobList[currentJobIndex].tradeQty.ToString();
            myInstance.transform.Find("inpBuySellAmount").GetComponent<InputField>().text = route.jobList[currentJobIndex].buySellValue.ToString();
            myInstance.transform.Find("Toggle").GetComponent<Toggle>().isOn = route.jobList[currentJobIndex].waitForCompletion;

        }
        else
        {
            //TODO annoying this isnt working deal with it later
            //myInstance.transform.Find("txtTradeRouteJobTitle").gameObject.GetComponent<Text>().text = "Trade Route Job - New";
        }

        //foreach(Transform child in )
        //Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        //GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        //refresh();
    }

    public void onPointerEnter()
    {
        GameObject.FindGameObjectWithTag("playerController").GetComponent<PlayerController>().onPointerOver();
    }
        public void onPointerExit()
    {
        GameObject.FindGameObjectWithTag("playerController").GetComponent<PlayerController>().onPointerExit();
    }
    public void onExitClick()
    {
        onPointerExit();
        Destroy(myInstance);
        //myInstance.SetActive(false);
        //timeBehavior.changeTimeScale(timeBehavior.lastTS);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        lastMousePosition = eventData.position;
        Debug.Log("Starting location " + eventData.position.ToString());
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        //Implement your funtionlity here
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentMousePosition = eventData.position;
        Vector2 diff = currentMousePosition - lastMousePosition;
        //Debug.Log("Diff is " + diff.ToString());
        RectTransform rect = GetComponent<RectTransform>();
 
        Vector3 newPosition = rect.position +  new Vector3(diff.x, diff.y, transform.position.z);
        Vector3 oldPos = rect.position;
        rect.position = newPosition;
        if(!IsRectTransformInsideSreen(rect))
        {
            rect.position = oldPos;
        }
        lastMousePosition = currentMousePosition;
        //data.delta
        //Debug.Log("Dragging.");
    }
    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0,0,Screen.width, Screen.height);
        foreach(Vector3 corner in corners)
        {
            if(rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if(visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }

    // Update is called once per frame
    void Update()
    {
        //disabling the update every day feature
        /*
        if (timeBehavior.gameDate.Day == lastUpdateDay)
        {
            return;
        }
        else
        {
            lastUpdateDay = timeBehavior.gameDate.Day;
            refresh();
        }
        if (contentGUI == null)
        {
            //Destroy(myInstance);
        }
        */
        

        //contentGUI.GetChild(0).gameObject.GetComponent<RawImage>().texture = (Texture)Resources.Load("Assets/icons/grain.png");

        
    }
}



