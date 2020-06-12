using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tradeJobSelection : MonoBehaviour
{
    public int index;
    public void onClick()
    {
        //clear other selections

        foreach (Transform child in GameObject.Find("tradeRouteDetails(Clone)").GetComponent<tradeRouteDetailsPanel>().contentGUI) 
        {
            Image thisPic = child.transform.Find("Panel").GetComponentInChildren<Image>();
            thisPic.color = new Color32(255, 255, 255, 45);
            //child.transform.Find("Panel").GetComponentInChildren<Image>()
        }

        Image pic = this.transform.parent.Find("Panel").GetComponentInChildren<Image>();
        pic.color = new Color32(255, 255, 255, 145);
        this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<tradeRouteDetailsPanel>().onSelect(this.transform.parent.transform);
        GameObject.Find("tradeRouteDetails(Clone)").GetComponent<tradeRouteDetailsPanel>().currentlySelected = index;
        GameObject.Find("tradeRouteDetails(Clone)").GetComponent<tradeRouteDetailsPanel>().enableButtons();
    }
}
