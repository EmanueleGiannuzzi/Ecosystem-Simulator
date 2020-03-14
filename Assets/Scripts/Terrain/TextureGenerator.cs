using UnityEngine;
using System.Collections;

using static BiomeHandler;

public static class TextureGenerator
{
	public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height)
	{
		Texture2D texture = new Texture2D(width, height);
		texture.filterMode = FilterMode.Point;
		texture.wrapMode = TextureWrapMode.Clamp;
		texture.SetPixels(colourMap);
		texture.Apply();
		return texture;
	}

	public static Texture2D TextureFromHeightMap(float[,] heightMap, BiomeType[] biomes)
	{
		int width = heightMap.GetLength(0);
		int height = heightMap.GetLength(1);

		Color[] colourMap = new Color[width * height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float currentHeight = heightMap[x, y];
				for (int i = 0; i < biomes.Length; i++)
				{
					if (currentHeight <= biomes[i].height)
					{
						colourMap[y * width + x] = biomes[i].color;
						break;
					}
				}
			}
		}

		return TextureFromColourMap(colourMap, width, height);
	}
}