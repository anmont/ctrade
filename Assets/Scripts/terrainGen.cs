
using UnityEngine;
 using System.Collections;
 using System.Runtime.InteropServices;
 using UnityEngine.AI;
 public class terrainGen : MonoBehaviour {
 public float Tiling = 0.5f;
 private bool active = false;
 public int mapHeight = 1000;
 
void Start() {
    Begin();    
}

 public void Begin()
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
         obj.transform.parent = this.transform;
         if (obj.GetComponent<Terrain> ()) {
             GenerateHeights (obj.GetComponent<Terrain> (), Tiling);
         }
     } else {
         GameObject obj = GameObject.Find ("Terrain");
         if (obj.GetComponent<Terrain> ()) {
             GenerateHeights (obj.GetComponent<Terrain> (), Tiling);
         }
     }
 }
 public void GenerateHeights(Terrain terrain, float tileSize)
 {
     Debug.Log ("Start_Height_Gen");
     float[,] heights = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];
     float[,] heightsFin = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];
     float[,] heightsOrig = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];

     float seed = (float)Random.Range(1,10000) / 10000f;
     float seedx = (float)Random.Range(30,70);
     float seedy = (float)Random.Range(30,70);
     
     for (int i = 0; i < terrain.terrainData.heightmapWidth; i++)
     {
         for (int k = 0; k < terrain.terrainData.heightmapHeight; k++)
         {
             //heights[i, k] = 0.25f + Mathf.PerlinNoise(((float)i / (float)terrain.terrainData.heightmapWidth) * tileSize, ((float)k / (float)terrain.terrainData.heightmapHeight) * tileSize);
             float pa = Random.Range(1,10000) / 10000;
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

     terrain.GetComponent<Transform>().position = new Vector3(-500,-5,-500);
     
     terrain.ApplyDelayedHeightmapModification();
     terrain.Flush();
     //heightsFin = terrain.terrainData.GetHeights(0,0,terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
     //Terrain obj = GameObject.Find ("TerrainA").GetComponent<Terrain>();
     //heightsOrig = obj.terrainData.GetHeights(0,0,obj.terrainData.heightmapWidth,obj.terrainData.heightmapHeight);
     //obj.terrainData.SetHeights(0,0, heights);
     NavMeshSurface mesh = globals.navMesh.gameObject.GetComponent<NavMeshSurface>();
     mesh.BuildNavMesh();
     Debug.Log("Fin");
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
}