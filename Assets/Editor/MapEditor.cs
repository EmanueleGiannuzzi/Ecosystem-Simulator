using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (MapGenerator))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        MapGenerator map = (MapGenerator)target;
        if(DrawDefaultInspector())//If any value was changed
        {
            map.GenerateMap();
        }
    }

}
