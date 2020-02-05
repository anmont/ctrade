using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btnAnchor : MonoBehaviour
{
    public GameObject myVessel;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(onClickAnchor);
        myVessel = this.transform.parent.GetComponent<btnShip>().thisVessel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onClickAnchor()
    {
        globals.openTradeWindow(myVessel.GetComponent<uiShipScript>().cityAtAnchor, myVessel);
    }
}
