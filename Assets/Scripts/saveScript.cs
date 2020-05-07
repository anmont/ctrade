using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;

public class saveScript : MonoBehaviour
{
    void Awake()
    {
        globals.saveInstance = this;
    }

    public void save()
    {
        List<saveShip> shipLists = new List<saveShip>(); 
        List<saveCity> cityLists = new List<saveCity>();
        saveGame newGameSave = new saveGame();
        //save ships
        foreach (GameObject vessel in globals.shipList)
        {
            saveShip thisVessel = new saveShip();
            
            //get ship details
            thisVessel.shipname = vessel.name;
            thisVessel.locationx = vessel.transform.position.x;
            thisVessel.locationy = vessel.transform.position.y;
            thisVessel.locationz = vessel.transform.position.z;
            thisVessel.shipInventory = vessel.GetComponent<uiShipScript>().shipInventory;

            shipLists.Add(thisVessel);
        }

        //save cities
        foreach (GameObject city in globals.cityList)
        {
            saveCity thisCity = new saveCity();

            thisCity.cityname = city.name;
            thisCity.locationx = city.transform.position.x;
            thisCity.locationy = city.transform.position.y;
            thisCity.locationz = city.transform.position.z;
            thisCity.cityInventory = city.GetComponent<uiCityScript>().cityInventory;
            thisCity.cityPopulation = city.GetComponent<uiCityScript>().cityPopulation;
            thisCity.cityProd1 = city.GetComponent<uiCityScript>().productionMaterial1;
            thisCity.cityProd2 = city.GetComponent<uiCityScript>().productionMaterial2;
            thisCity.cityProd3 = city.GetComponent<uiCityScript>().productionMaterial3;

            cityLists.Add(thisCity);
        }

        //save player history
        newGameSave.mapseedx = globals.mapSeed.x;
        newGameSave.mapseedy = globals.mapSeed.y;
        newGameSave.mapseedz = globals.mapSeed.z;

        //save wealth and time
        newGameSave.cumTime = timeBehavior.cumulativeTime;
        newGameSave.playerCash = globals.playerCash;

        //save automated routes
        
        //assign temp values to new save record
        newGameSave.shipSaveFormat = shipLists;
        newGameSave.citySaveFormat = cityLists;

        //make sure the save director works


        //write save file
        var binaryFormatter = new BinaryFormatter();
        string file = StaticClass.savePath + StaticClass.saveFileName;
        FileInfo fileP = new FileInfo(StaticClass.savePath);
        fileP.Directory.Create();
        Debug.Log(file);
        using (var fileStream = File.Create(file))
        {
            binaryFormatter.Serialize(fileStream, newGameSave);
        }
 
        Debug.Log("Data Saved");
    }
    public void load()
    {
        Debug.Log("Load was called");
        saveGame loadGameSave;
        string file = StaticClass.savePath + StaticClass.saveFileName;

        var binaryFormatter = new BinaryFormatter();
        using (var fileStream = File.Open(file, FileMode.Open))
        {
            loadGameSave = (saveGame)binaryFormatter.Deserialize(fileStream);
        }

        globals.mapSeed = new Vector3(loadGameSave.mapseedx,loadGameSave.mapseedy, loadGameSave.mapseedz);


        terrainGen.reGenerateHeights();

        foreach (saveShip ship in loadGameSave.shipSaveFormat)
        {
            Vector3 location = new Vector3(ship.locationx,ship.locationy,ship.locationz);
            GameObject newShip = globals.createVessel(location,0,ship.shipname);
            newShip.GetComponent<uiShipScript>().shipInventory = ship.shipInventory;

            Debug.Log(newShip.name + " was created at location " + ship.locationx + "," + ship.locationx + "," + ship.locationx);
        }

        foreach (saveCity city in loadGameSave.citySaveFormat)
        {
            Vector3 location = new Vector3(city.locationx, city.locationy, city.locationz);
            GameObject newCity = globals.createCity(location,"y");
            newCity.name = city.cityname;
            newCity.GetComponent<uiCityScript>().cityInventory = city.cityInventory;
            newCity.GetComponent<uiCityScript>().cityPopulation = city.cityPopulation;
            newCity.GetComponent<uiCityScript>().productionMaterial1 = city.cityProd1;
            newCity.GetComponent<uiCityScript>().productionMaterial2 = city.cityProd2;
            newCity.GetComponent<uiCityScript>().productionMaterial3 = city.cityProd3;

        }


        // TODO globals.createCity(landHitLoc);

        timeBehavior.cumulativeTime = loadGameSave.cumTime;
        globals.playerCash = loadGameSave.playerCash;
        //globals.mapSeed = new Vector3(loadGameSave.mapseedx,loadGameSave.mapseedy, loadGameSave.mapseedz);

        

        Debug.Log("Data Loaded");


    }

}

[System.Serializable]
public class saveStructure 
{
    //public List<Shipl
}

[System.Serializable]
public class saveShip
{
    public string shipname;
    public float locationx;
    public float locationy;
    public float locationz;

    public List<tradeGoods> shipInventory;
}

[System.Serializable]
public class saveCity
{
    public string cityname;
    public int cityPopulation;
    public int cityProd1;
    public int cityProd2;
    public int cityProd3;
    public float locationx;
    public float locationy;
    public float locationz;

    public List<tradeGoods> cityInventory;
}

[System.Serializable]
public class saveGame
{
    public List<saveShip> shipSaveFormat;
    public List<saveCity> citySaveFormat;
    public float mapseedx; 
    public float mapseedy;
    public float mapseedz;
    public float cumTime; 
    public double playerCash;

}