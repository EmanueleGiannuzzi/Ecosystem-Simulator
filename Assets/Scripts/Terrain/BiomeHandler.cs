using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeHandler
{
    private static BiomeType DEFAULT_BIOME = new BiomeType("Deafult Biome", -1, Color.black, false);

    [System.Serializable]
    public struct BiomeType
    {
        public string name;
        public float height;
        public Color color;
        public bool isWalkable;

        public BiomeType(string name, float height, Color color, bool isWalkable) : this()
        {
            this.name = name;
            this.height = height;
            this.color = color;
            this.isWalkable = isWalkable;
        }
    }

    public static BiomeType getBiomeFromHeight(BiomeType[] biomes, float height)
    {
        for (int i = 0; i < biomes.Length; i++)
        {
            BiomeHandler.BiomeType biome = biomes[i];
            if (height <= biome.height)
            {
                return biome;
            }
        }

        return DEFAULT_BIOME;
    }
}