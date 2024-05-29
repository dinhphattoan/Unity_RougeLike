using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBackground : MonoBehaviour
{

    [Header("Noisemap attributes")]
    [Space]
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    public TerrainType[] regions;
    //Tilemap background pitch black with water stream and brick ruines, with output pipe, ...
    public Tilemap tilemap_Bacground;

    [Header("Preload Resources")]
    public Texture2D texture2D;
    public Texture2D texture2D_reverse;
    [SerializeField] List<Sprite> resources;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void GenerateMap()
    {
        ////Load resouces
        resources.Clear();
        tilemap_Bacground.ClearAllTiles();
        resources.Add(Methods.GetSpriteFromTexture2DAtPosition(texture2D, 1, new Vector2Int(8, 8), new Vector2(0, 0)));//Null tile
        resources.Add(Methods.GetSpriteFromTexture2DAtPosition(texture2D, 1, new Vector2Int(8, 8), new Vector2(0, 3)));
        resources.Add(Methods.GetSpriteFromTexture2DAtPosition(texture2D_reverse, 1, new Vector2Int(8, 8), new Vector2(0, 4)));
        //Generate the noise map for random nature distribution.
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        Color[] colourMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colourMap[y * mapWidth + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }
        int[,] intMap = MapGenerator.ColorMapToInt(colourMap, regions.ToList(), mapWidth, mapHeight);
        drawSpriteOnTileMap(ref tilemap_Bacground, intMap, 1);
        //Draw the rest are null tile
        for(int i = 0; i < intMap.GetLength(1); i++)
        {
            for(int j = 0; j < intMap.GetLength(0); j++)
            {
                if(intMap[i,j]==0)
                {
                    Methods.DrawSpriteOnTile(ref tilemap_Bacground, new Vector2Int(i, j), resources[0]);
                }
            }
        }
    }
    public void drawSpriteOnTileMap(ref Tilemap tilemap,int[,] intMap,int regionId)
    {
        for(int i = 0; i < intMap.GetLength(1); i++)
        {
            for(int j = 0; j < intMap.GetLength(0); j++)
            {
                if(intMap[i,j]==regionId)
                {
                    Methods.DrawSpriteOnTile(ref tilemap, new Vector2Int(i, j), resources[UnityEngine.Random.Range(1,2)]);
                }
            }
        }
    }
}
