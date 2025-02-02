﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class cityDetailsPanel : MonoBehaviour
{
    public Vector2 lastMousePosition;
    //public GameObject cityToTrade;
    public GameObject cityDetail;
    public GameObject myInstance;
    public Transform contentGUI;
    public bool isAi;
    public int lastUpdateDay = 44;




    // Start is called before the first frame update
    void Start()
    {
        myInstance = this.gameObject;

        if (cityDetail.tag == "ai")
        {
            isAi = true;
        }
        else
        {
            isAi = false;
        }

        //this.gameObject.GetComponent<panel..onClick.AddListener(navigateTo);
        //EventTrigger triggerA = GetComponent<EventTrigger>();
        myInstance.GetComponentInChildren<Button>().onClick.AddListener(onExitClick);
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

        Text[] uiTexts = this.GetComponentsInChildren<Text>();

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




        //foreach(Transform child in )
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);
        //GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in allChildren)
        {
            if (child.transform.gameObject.name == "Content")
            {
                contentGUI = child.transform;
            }
        }
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
        if (timeBehavior.gameDate.Day == lastUpdateDay)
        {
            return;
        }
        else
        {
            lastUpdateDay = timeBehavior.gameDate.Day;
        }
        if (cityDetail == null)
        {
            Destroy(myInstance);
        }

        
        int goodsIndex = 0;
        while (goodsIndex < 18)
        {
            Transform goods = contentGUI.GetChild(goodsIndex).transform;
            Text[] textlist = goods.gameObject.GetComponentsInChildren<Text>();


                textlist[0].text = Mathf.Round((float)cityDetail.GetComponent<uiCityScript>().cityInventory[goodsIndex].quantity).ToString(); //total amount of goods to trade
                if (textlist[0].text == "0")
                {
                    goods.gameObject.SetActive(false);
                }
                else
                {
                    goods.gameObject.SetActive(true);
                }

            goodsIndex++;
        }

        //contentGUI.GetChild(0).gameObject.GetComponent<RawImage>().texture = (Texture)Resources.Load("Assets/icons/grain.png");

        
    }
}


