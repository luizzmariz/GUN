using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256; 

    public float scale = 20f;
    public float offsetX = 100f;
    public float offsetY = 100f;

    //public Renderer renderer;
    public Material material;

    void Start()
    {
        //renderer = GetComponent<Renderer>();
        material.mainTexture = GenerateTexture();
    }

    void Update()
    {
        
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; x < height; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        Color color = new Color(sample, sample, sample);
        return color;
    }
}
