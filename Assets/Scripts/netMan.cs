using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netMan : NetworkManager
{
    public override void OnStartServer()
    {
        globals.terrainGenI.GetComponent<terrainGen>().Begin();
        Debug.Log("i waited for the server start");
    }
}
