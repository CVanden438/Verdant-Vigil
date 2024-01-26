using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PerlinNoise : MonoBehaviour
{
    public TileBase water;
    public TileBase sand;
    public TileBase forest;
    public Tilemap map;
    private int height = 200;
    private int width = 200;

    public float scale = 1f;
    public float xOffset = 100f;
    public float yOffset = 100f;

    // Start is called before the first frame update
    void Start()
    {
        // GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase tile = CaulculateTile(x, y);
                Vector3Int pos = new Vector3Int(x, y, 0);
                map.SetTile(pos, tile);
            }
        }
    }

    private TileBase CaulculateTile(int x, int y)
    {
        float xCoord = (float)x / width * scale + xOffset;
        float yCoord = (float)y / height * scale + yOffset;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        if (sample <= 0.5)
        {
            return water;
        }
        if (sample <= 0.6)
        {
            return sand;
        }
        return forest;
    }
}
