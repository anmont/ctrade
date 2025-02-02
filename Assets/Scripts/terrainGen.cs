﻿
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.AI;
 public class terrainGen : MonoBehaviour {
 public float Tiling = 0.5f;
 private bool active = false;
 public int mapHeight = 1000;
 public static List<string> cityList = new List<string>();
 public bool newGame = true;

 
void Start() {
    if (StaticClass.saveFileName == "newgame")
    {
        // Start a new city
        //Debug.Log("I am starting the new city Generation");
        newGame = true;
        GenerateHeights();
    }
    else if (StaticClass.saveFileName == null)
    {
        //This path is for scene debugging only it shou8ld never4 be run in prod
        newGame = true;
        GenerateHeights();

    }
    else
    {
        //Load Saved Game
        Debug.Log("I am loading the game " + StaticClass.savePath + StaticClass.saveFileName);
        newGame = false;
        globals.saveInstance.load();
        
    }
    Begin();    

    cityList.Add("Cloudfall");
    cityList.Add("Honeyfair");
    cityList.Add("Nighthallow");
    cityList.Add("Redbreak");
    cityList.Add("Windward");
    cityList.Add("Summerwharf");
    cityList.Add("Fallcoast");
    cityList.Add("Eastforest");
    cityList.Add("Tradescar");
    cityList.Add("Darkstorm");
    cityList.Add("Mistgulch");
    cityList.Add("Shroudwind");
    cityList.Add("Mossmount");
    cityList.Add("Oceanshear");
    cityList.Add("Lakegulch");
    cityList.Add("Stormglen");
    cityList.Add("Dewfort");
    cityList.Add("Mythband");
    cityList.Add("Oceanhill");
    cityList.Add("Wolfbarrow");
    cityList.Add("Ebonpass");
    cityList.Add("Thornbury");
    cityList.Add("Timbershire");
    cityList.Add("Stagpost");
    cityList.Add("Westcross");
    cityList.Add("Windhallow");
    cityList.Add("Riverforest");
    cityList.Add("Midwood");
    cityList.Add("Boulderfalls");
    cityList.Add("Saltpeak");
    cityList.Add("Whitwell");
}

 public void Begin()
 {
    /*if (StaticClass.saveFileName == "newgame")
    {
        if (active == false) {
            TerrainData terrainData = new TerrainData ();
            const int size = 513;
            terrainData.heightmapResolution = size;
            terrainData.size = new Vector3 (1000, mapHeight, 1000);

            terrainData.heightmapResolution = 513;
            terrainData.baseMapResolution = 1024;
            terrainData.SetDetailResolution (1024, 32);
            Terrain.CreateTerrainGameObject (terrainData);
            GameObject obj = GameObject.Find ("Terrain");
            //obj.transform.parent = this.transform;
            if (obj.GetComponent<Terrain> ()) {
                GenerateHeights (obj.GetComponent<Terrain> (), Tiling);
            }
        } 
        else 
        {
            GameObject obj = GameObject.Find ("Terrain");
            if (obj.GetComponent<Terrain> ()) {
                GenerateHeights (obj.GetComponent<Terrain> (), Tiling);
            }
        }
    }*/
    //if (newGame)

    
 }

public static void heightGenerator (Vector3 mapSeed)
{
    float peakCap = 0.6f;
    GameObject terrainObj = GameObject.Find("Terrain");
    Terrain terrain = terrainObj.GetComponent<Terrain>();

    //Debug.Log ("Start_Height_Gen_v2");
    TerrainData terrainDataSet = terrain.terrainData;
    const int size = 513;
    terrainDataSet.heightmapResolution = size;
    terrainDataSet.size = new Vector3 (1000, 50, 1000);
    terrainDataSet.heightmapResolution = 513;
    terrainDataSet.baseMapResolution = 1024;
    terrainDataSet.SetDetailResolution (1024, 32);
    //Terrain.CreateTerrainGameObject (terrainDataSet);

    //float tileSize = 0.5f;
    //GameObject terrainObj = GameObject.Find("Terrain");
    //Terrain terrain = terrainObj.GetComponent<Terrain>();

    float[,] heights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
    float[,] heightsFin = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
    float[,] heightsOrig = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

    float seed;
    float seedx;
    float seedy;

    if (mapSeed == Vector3.zero)
    {
        seed = (float)Random.Range(1,10000) / 10000f;
        seedx = (float)Random.Range(30,70);
        seedy = (float)Random.Range(30,70);
    }
    else
    {
        seed = mapSeed.x;
        seedx = mapSeed.y;
        seedy = mapSeed.z;
    }

    //Debug.Log("seedx = " + seed.ToString());
    //Debug.Log("seedy = " + seedx.ToString());
    //Debug.Log("seedz = " + seedy.ToString());

    globals.mapSeed = new Vector3(seed,seedx,seedy);

    for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
    {
        for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
        {
            heights[i, k] = Mathf.PerlinNoise(((float)i + seed)/seedx, ((float)k + seed)/seedy);
            if (heights[i, k] > peakCap)
            {
                heights[i, k] = peakCap;
            }
            // height peak 
            //heights[i, k] = 0.25f + Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize);
             //float pa = Random.Range(1,10000) / 10000;
             //heights[i, k] = Mathf.PerlinNoise((float)i / (512f + pa), (float)k / (512f + pa));
             //heights[i, k] = Mathf.PerlinNoise(((float)i + seed)/seedx, ((float)k + seed)/seedy)/10;
             //Debug.Log(heights[i,k].ToString() + "value of i:k" + i.ToString() + "," + k.ToString());
             //Debug.Log("k = " + k.ToString());

             //float pa = Random.Range(1,50000); 
             //float pb = Random.Range(1,50000);

             //float b = Random.Range(1,10000);
             //float c = b/10000;
             //heights[i,k] = c;
             //heights[i, k] *= makeMask( terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight, i, k, heights[i, k] );
        }
    }
    //terrain.terrainData.SetHeightsDelayLOD(0, 0, heights);
    terrain.terrainData.SetHeights(0, 0, heights);

    terrain.GetComponent<Transform>().position = new Vector3(-500, -28f,-500);

    //terrain.ApplyDelayedHeightmapModification();
    terrain.Flush();
    //heightsFin = terrain.terrainData.GetHeights(0,0,terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
    //Terrain obj = GameObject.Find ("TerrainA").GetComponent<Terrain>();
    //heightsOrig = obj.terrainData.GetHeights(0,0,obj.terrainData.heightmapWidth,obj.terrainData.heightmapHeight);
    //obj.terrainData.SetHeights(0,0, heights);
    NavMeshSurface mesh = globals.navMesh.GetComponent<NavMeshSurface>();
    //mesh.BuildNavMesh.buildHeightMesh();

    mesh.BuildNavMesh();

    //terrain.transform.position += new Vector3(0,0.5f,0);

    Vector3 navMeshLoc = GameObject.FindGameObjectWithTag("navMeshSurface").GetComponent<Transform>().position;
    navMeshLoc = new Vector3(0,-0.5f,0);
    //generateCities();
    
    //GameObject waterlayer = GameObject.Find("Water");
    //waterlayer.transform.position = new Vector3(waterlayer.transform.position.x,-30f,waterlayer.transform.position.z);

}



 public void orig_GenerateHeights(Terrain terrain, float tileSize)
 {
     
     Debug.Log ("Start_Height_Gen");
     float[,] heights = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
     float[,] heightsFin = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];
     float[,] heightsOrig = new float[terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution];

     float seed = (float)Random.Range(1,10000) / 10000f;
     float seedx = (float)Random.Range(30,70);
     float seedy = (float)Random.Range(30,70);

    Debug.Log("seedx = " + seed.ToString());
    Debug.Log("seedy = " + seedx.ToString());
    Debug.Log("seedz = " + seedy.ToString());

    globals.mapSeed = new Vector3(seed,seedx,seedy);

     
     for (int i = 0; i < terrain.terrainData.heightmapResolution; i++)
     {
         for (int k = 0; k < terrain.terrainData.heightmapResolution; k++)
         {
             //heights[i, k] = 0.25f + Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize);
             //float pa = Random.Range(1,10000) / 10000;
             //heights[i, k] = Mathf.PerlinNoise((float)i / (512f + pa), (float)k / (512f + pa));
             heights[i, k] = Mathf.PerlinNoise(((float)i + seed)/seedx, ((float)k + seed)/seedy);

             //float pa = Random.Range(1,50000);
             //float pb = Random.Range(1,50000);

             //float b = Random.Range(1,10000);
             //float c = b/10000;
             //heights[i,k] = c;
             //heights[i, k] *= makeMask( terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight, i, k, heights[i, k] );
         }
     }
     terrain.terrainData.SetHeightsDelayLOD(0, 0, heights);
     //terrain.terrainData.SetHeights(0, 0, heights);

     terrain.GetComponent<Transform>().position = new Vector3(-500, -5,-500);
     
     //terrain.ApplyDelayedHeightmapModification();
     terrain.Flush();
     //heightsFin = terrain.terrainData.GetHeights(0,0,terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
     //Terrain obj = GameObject.Find ("TerrainA").GetComponent<Terrain>();
     //heightsOrig = obj.terrainData.GetHeights(0,0,obj.terrainData.heightmapWidth,obj.terrainData.heightmapHeight);
     //obj.terrainData.SetHeights(0,0, heights);
     NavMeshSurface mesh = globals.navMesh.GetComponent<NavMeshSurface>();
     //mesh.BuildNavMesh.buildHeightMesh();

     mesh.BuildNavMesh();

     //terrain.transform.position += new Vector3(0,0.5f,0);

     Vector3 navMeshLoc = GameObject.FindGameObjectWithTag("navMeshSurface").GetComponent<Transform>().position;
     navMeshLoc = new Vector3(0,-0.5f,0);
     //generateCities();
     

     StartCoroutine(generateCitiesandVessels());
     Debug.Log("Fin");
     //terrain.GetComponent<Transform>().position = new Vector3(-500, -5.5f,-500);
     //terrain.GetComponent<Transform>().position = new Vector3(-500, -5f,-500);
 }


 public void GenerateHeights()
 {

     heightGenerator(Vector3.zero);
     StartCoroutine(generateCitiesandVessels());
     Debug.Log("Fin");
     //terrain.GetComponent<Transform>().position = new Vector3(-500, -5.5f,-500);
     //terrain.GetComponent<Transform>().position = new Vector3(-500, -5f,-500);
 }

 public static void reGenerateHeights()
 {
     heightGenerator(globals.mapSeed);

     Debug.Log("Fin regen");
 }

//public static void generateCities ()
IEnumerator generateCitiesandVessels()
{

    int city = 0;
    int vessels = 0;
    Vector3 landHitLoc = Vector3.zero;
    // drop a random raycast, if it hits land
    while (city < 10)
    {
        //raycast(Vector2.zero);
        RaycastHit hit = raycast(Vector2.zero);
        if (hit.collider.name.ToString() == "Water")
        {
            //Debug.Log("found water at " + hit.point.ToString());
            //hit.point;
            bool landHit = false;
            //RaycastHit water;
            bool done = false;
            
            while (!done && !landHit)
            {
                //Vector3 cardinalN = hit.point + new Vector3(0,0,0);
                RaycastHit cardinalNHit;
                //Vector3 cardinalS = hit.point + new Vector3(0,0,0);
                RaycastHit cardinalSHit;
                //Vector3 cardinalW = hit.point + new Vector3(0,0,0);
                RaycastHit cardinalWHit;
                //Vector3 cardinalE = hit.point + new Vector3(0,0,0);
                RaycastHit cardinalEHit;

                //ector3 heightCorrection = new Vector3(0,0,0);

                hit.point.ToString();

                Physics.Raycast(hit.point, Vector3.forward, out cardinalNHit , 30f);
                //Debug.DrawRay(hit.point, Vector3.forward, Color.red, 20.0f, true);
                if (cardinalNHit.collider != null)
                {
                    if (cardinalNHit.collider.name == "Terrain")
                    {
                        //Debug.Log(cardinalNHit.point.ToString());
                        if(cardinalNHit.point != Vector3.zero)
                        {
                            landHit = true;
                            landHitLoc = cardinalNHit.point;
                        }
                        
                    }
                }

                Physics.Raycast(hit.point, Vector3.back, out cardinalSHit , 30f);
                //Debug.DrawRay(hit.point, Vector3.back, Color.red, 20.0f, true);
                if (cardinalSHit.collider != null)
                {
                    if (cardinalSHit.collider.name == "Terrain")
                    {
                        //Debug.Log(cardinalSHit.point.ToString());
                        if(cardinalSHit.point != Vector3.zero)
                        {
                            landHit = true;
                            landHitLoc = cardinalSHit.point;
                        }
                    }
                }

                 Physics.Raycast(hit.point, Vector3.left, out cardinalWHit , 30f);
                 //Debug.DrawRay(hit.point, Vector3.left, Color.red, 20.0f, true);
                if (cardinalWHit.collider != null)
                {
                    if (cardinalWHit.collider.name == "Terrain")
                    {
                        //Debug.Log(cardinalWHit.point.ToString());
                        if(cardinalWHit.point != Vector3.zero)
                        {
                            landHit = true;
                            landHitLoc = cardinalWHit.point;
                        }
                    }
                }
                Physics.Raycast(hit.point, Vector3.right, out cardinalEHit , 30f);
                //Debug.DrawRay(hit.point, Vector3.right, Color.red, 20.0f, true);
                if (cardinalEHit.collider != null)
                {
                    if (cardinalEHit.collider.name == "Terrain")
                    {
                        //Debug.Log(cardinalEHit.point.ToString());
                        if(cardinalEHit.point != Vector3.zero)
                        {
                            landHit = true;
                            landHitLoc = cardinalEHit.point;
                        }
                    }
                }

                done = true;

            }
        yield return null;

        }

        if (landHitLoc != Vector3.zero)
        {
            if (cityLocationValid(landHitLoc))
            {
                //create new city
                //Debug.Log("Create city at " + landHitLoc);
                globals.createCity(landHitLoc);
                city ++;
            }
        }
        //city ++;
        }


    // create the new starter ship
    globals.createVessel(globals.cityList[0].transform.position,0);
    
    // create the aiVessels
    while (vessels < globals.aiVessels)
    {
        globals.createAiVessel(globals.cityList[vessels].gameObject.transform.position);
        vessels ++;
    }

    // iterate throught the list of city locations and make sure its not closer than xxx to any city
    // drop 4 raycasts at each cardinal direct and see if it hits water
    // if water is hit, drop several other lines at small intervals to determine the exact spot
    // place a prefab and autoname it, add it to the cities collections

    //Here we will temporarily overide the create vessel function and load the initial vessel list from the saved objects


    
}
public void tempCreateSmallVessel()
{
    globals.createVessel(globals.cityList[0].gameObject.transform.position,0);
}
public void tempCreateLargeVessel()
{
    globals.createVessel(globals.cityList[0].gameObject.transform.position,1);
}

public void tempCreateMediumVessel()
{
    globals.createVessel(globals.cityList[0].gameObject.transform.position,2);
}
public static bool cityLocationValid(Vector3 loc)
{
    bool ret = true;

    foreach (GameObject city in globals.cityList)
    {
        if (Vector3.Distance(city.transform.position,loc) < 100)
        {
            ret = false;
        }
    }


    return ret;
}

public static RaycastHit raycast (Vector2 loc)
{
    RaycastHit hit;

    Vector3 origin = new Vector3(0,15f,0); //start 
    Vector3 direction = Vector3.down;// new Vector3(90f,0,0);

    float maxDist = 20f;

    float a;
    float b;

    if (loc == Vector2.zero)
    {
        //randomize
        a = (float)Random.Range(1,1000)-500;
        b = (float)Random.Range(1,1000)-500;

        origin.x = a;
        origin.z = b;
    }
    else
    {
        origin = new Vector3(loc.x, 15f, loc.y);
    }

    Physics.Raycast(origin , direction, out hit , maxDist);
    //Debug.Log(hit.collider.name.ToString());

    return hit;
}

 public static float makeMask( int width, int height, int posX, int posY, float oldValue ) {
     int minVal = ( ( ( height + width ) / 2 ) / 100 * 2 );
     int maxVal = ( ( ( height + width ) / 2 ) / 100 * 10 );

     if( getDistanceToEdge( posX, posY, width, height ) <= minVal ) {
         return 0;
     } else if( getDistanceToEdge( posX, posY, width, height ) >= maxVal ) {
         return oldValue;
     } else {
         float factor = getFactor( getDistanceToEdge( posX, posY, width, height ), minVal, maxVal );
         return oldValue * factor;
     }
 }
 
 private static float getFactor( int val, int min, int max ) {
     int full = max - min;
     int part = val - min;
     float factor = (float)part / (float)full;
     return factor;
 }
 
 public static int getDistanceToEdge( int x, int y, int width, int height ) {
     int[] distances = new int[]{ y, x, ( width - x ), ( height - y ) };
     int min = distances[ 0 ];
     foreach( var val in distances ) {
         if( val < min ) {
             min = val;
         }
     }
     return min;
 }
 public static string cityNameGen()
 {
     string ret = "";
    





    return ret;
 }
}