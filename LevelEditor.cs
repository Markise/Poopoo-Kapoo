using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorMappings;

    // Use this for initialization
    void Start() {

        GenerateLevel();
    }

    void GenerateLevel()
    {

        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if(pixelColor.a == 0)
        {
            //nopixel there checked alpha transparency
            return;
        }

        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector3 pos = new Vector3(x,y,0f);//displays it horizontally
                Instantiate(colorMapping.prefab, pos, colorMapping.prefab.transform.rotation,transform);
            }
        }
    }
	
}
