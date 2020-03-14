using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static BiomeHandler;

public class TerrainGenerator : MonoBehaviour
{
    public bool autoUpdate;

    public Vector2Int mapSize;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public BiomeType[] biomes;

    float[,] noiseMap;


    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if(transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        noiseMap = Noise.GenerateNoiseMap(mapSize.x, mapSize.y, seed, noiseScale, octaves, persistance, lacunarity, offset);
        
        TerrainRenderer terrainRenderer = FindObjectOfType<TerrainRenderer>();
        terrainRenderer.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureFromHeightMap(noiseMap, biomes));
    }

    private void OnValidate()
    {
        if (mapSize.x < 1)
        {
            mapSize.x = 1;
        }
        if (mapSize.y < 1)
        {
            mapSize.y = 1;
        }
        if(lacunarity < 1)
        {
            lacunarity = 1;
        }
        if(octaves <= 0)
        {
            octaves = 0;
        }
    }

    public void resetBiomes()
    {
        BiomeType water = new BiomeType("Water", 0.4f, new Color(0.1098039f, 0.6392157f, 0.9254902f), false);
        BiomeType shore = new BiomeType("Shore", 0.5f, new Color(0.9490196f, 0.8196079f, 0.4196078f), true);
        BiomeType grass = new BiomeType("Grass", 0.8f, new Color(0.1852135f, 0.7169812f, 0.1589534f), true);
        BiomeType hill = new BiomeType("Hill", 1.0f, new Color(0.05882353f, 0.4235294f, 0.01176471f), true);

        biomes = new BiomeType[] { water, shore, grass, hill };
    }

    public BiomeType getBiomeFromPos(Vector3 pos)
    {
        float heightValue = noiseMap[(int)pos.x, (int)pos.z];//TODO: Check z or y
        return BiomeHandler.getBiomeFromHeight(biomes, heightValue);
    }
}
