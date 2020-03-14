using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
    TerrainGenerator terrainGen;

    public override void OnInspectorGUI()
    {
        if(DrawDefaultInspector())//If any value was changed
        {
            if (terrainGen.autoUpdate)
            {
                terrainGen.GenerateMap();
            }
        }

        if (GUILayout.Button("Refresh"))
        {
            terrainGen.GenerateMap();
        }
        if(GUILayout.Button("Reset Biomes"))
        {
            terrainGen.resetBiomes();
        }
    }

    void OnEnable()
    {
        terrainGen = (TerrainGenerator)target;
    }

}
