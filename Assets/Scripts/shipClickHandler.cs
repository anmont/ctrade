using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipClickHandler : MonoBehaviour
{
    
    public GameObject vesselInstance;
    public int vesselStorageSize;
    public int vesselClassification;
    public List<tradeGoods> shipInventory = new List<tradeGoods>() { 
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
    // Start is called before the first frame update
    void Start()
    {
        vesselInstance = this.gameObject;
        //vesselInstance.GetComponent<vesselScript>().shipInventory[0].quantity = 10;
    
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
