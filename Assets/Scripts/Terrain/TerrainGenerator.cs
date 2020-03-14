using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Transform tilePrefab;

    public bool autoUpdate;

    public Vector2Int mapSize;
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public BiomeHandler.BiomeType[] biomes;


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

        float[,] noiseMap = Noise.GenerateNoiseMap(mapSize.x, mapSize.y, seed, noiseScale, octaves, persistance, lacunarity, offset);
        
        for(int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePos = new Vector3(-mapSize.x/2 + 0.5f + x, 0, -mapSize.y/2 + 0.5f + y);
                Transform newTile = (Transform)Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90));
                newTile.parent = mapHolder;

                BiomeHandler.BiomeType biome = BiomeHandler.getBiomeFromHeight(biomes, noiseMap[x, y]);
                Renderer tileRenderer = newTile.GetComponent<Renderer>();
                Material tileMaterial = new Material(tileRenderer.sharedMaterial);
                tileMaterial.color = biome.color;
                tileRenderer.sharedMaterial = tileMaterial;

                /*float currentHeight = noiseMap[x, y];

                for (int i = 0; i < biomes.Length; i++)
                {
                    BiomeHandler.BiomeType biome = biomes[i];
                    if (currentHeight <= biome.height)
                    {
                        Renderer tileRenderer = newTile.GetComponent<Renderer>();
                        Material tileMaterial = new Material(tileRenderer.sharedMaterial);
                        tileMaterial.color = biome.color;
                        tileRenderer.sharedMaterial = tileMaterial;
                        break;
                    }
                }*/
            }
        }
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

    
}
