using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class tradeGoods
{
    public string productName { get; set; }
    //public string prefabName { get; set; }
    public int tier { get; set; }
    public double quantity { get; set; }
    public double perPersonPerYear { get; set; }
    public double lastConsumptionQty { get; set; }
    public double lastProductionQty { get; set; }
    public float cityBuyPrice { get; set; }
    public double citySellPrice { get; set; }


}
