using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode
    {
        NoiseMap,ColorMap
    };

    public DrawMode drawmode;

    public int MapWidth;
    public int MapHeight;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public int lacunarity;
    public int seed;
    public Vector2 offset;

    public terrainTypes[] regions;

    public bool autoUpdate;

    public void generateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, seed, noiseScale, octaves, persistance, lacunarity,offset);

        Color[] colorMap = new Color[MapHeight * MapWidth];

        for(int y = 0; y < MapHeight ; y++){
            for(int x = 0; x < MapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * MapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawmode == DrawMode.NoiseMap)
        {
            display.drawTexture(TextureGenerator.textureFromHeightMap(noiseMap));
        }
        else if(drawmode == DrawMode.ColorMap)
        {
            display.drawTexture(TextureGenerator.textureFromColorMap(colorMap,MapWidth,MapHeight));
        }
    }

    private void OnValidate()
    {
        if (MapWidth < 1)
        {
            MapWidth = 1;
        }
        if (MapHeight < 1)
        {
            MapHeight = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }

    }

}

[System.Serializable]
public struct terrainTypes
{
    public string name;
    public float height;
    public Color color;
}

