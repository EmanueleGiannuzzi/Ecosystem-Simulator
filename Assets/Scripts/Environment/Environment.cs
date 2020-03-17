using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static BiomeHandler;

public class Environment : MonoBehaviour
{
    public bool GetWaterTileAround(Vector3 position, float radius, out Vector2Int result)
    {
        TerrainGenerator terrainGenerator = FindObjectOfType<TerrainGenerator>();

        int posX = (int)position.x;
        int posY = (int)position.z;

        int sightSize = (int)(radius * 2);

        for(int x = 0; x < sightSize; x++)
        {
            for (int y = 0; y < sightSize; y++)
            {
                if (terrainGenerator.GetBiomeFromPos(x, y).name == "Water")
                {
                    result = new Vector2Int(x, y);
                    return true;
                }
            }
        }

        result = Vector2Int.zero;
        return false;
    }
}
