using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator map = (MapGenerator)target;

        if (DrawDefaultInspector()){
            map.generateMap();
        }

        if (GUILayout.Button("Generate"))
        {
            map.generateMap();
        }
        
    }
}
