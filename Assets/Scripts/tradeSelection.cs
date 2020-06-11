using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tradeSelection : MonoBehaviour
{
    public float index;
    public void onClick()
    {
        //clear other selections

        foreach (Transform child in GameObject.Find("tradeRoutePanel").GetComponent<tradeRoutePanels>().contentGUI) 
        {
            Image thisPic = child.transform.Find("Panel").GetComponentInChildren<Image>();
            thisPic.color = new Color32(255, 255, 255, 45);
            //child.transform.Find("Panel").GetComponentInChildren<Image>()
        }

        Image pic = this.transform.parent.Find("Panel").GetComponentInChildren<Image>();
        pic.color = new Color32(255, 255, 255, 145);
        this.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent.GetComponent<tradeRoutePanels>().onSelect(this.transform.parent.transform);
        GameObject.Find("tradeRoutePanel").GetComponent<tradeRoutePanels>().currentlySelected = index;
        GameObject.Find("tradeRoutePanel").GetComponent<tradeRoutePanels>().enableButtons();
    }
}
