using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    private Tilemap map1;

    // [SerializeField]
    // private Tilemap map2;

    // [SerializeField]
    // private Tilemap map3;

    // [SerializeField]
    // private Tilemap map4;
    private BoundsInt bounds1;

    // private BoundsInt bounds2;
    // private BoundsInt bounds3;
    // private BoundsInt bounds4;
    private TileBase[,] tiles1;
    private TileBase[] flattenedTiles;
    private TileBase[,] nullTiles;
    private TileBase[] flattenedNullTiles;

    [SerializeField]
    private TileBase testTile;

    // private TileBase[,] tiles2;
    // private TileBase[,] tiles3;
    // private TileBase[,] tiles4;

    // Start is called before the first frame update
    void Start()
    {
        map1.CompressBounds();
        bounds1 = map1.cellBounds;
        // bounds2 = map2.cellBounds;
        // bounds3 = map3.cellBounds;
        // bounds4 = map4.cellBounds;
        tiles1 = new TileBase[bounds1.size.x, bounds1.size.y];
        nullTiles = new TileBase[bounds1.size.x, bounds1.size.y];

        // tiles2 = new TileBase[bounds2.size.x, bounds2.size.y];
        // tiles3 = new TileBase[bounds3.size.x, bounds3.size.y];
        // tiles4 = new TileBase[bounds4.size.x, bounds4.size.y];
        for (int y = bounds1.min.y; y < bounds1.max.y; y++)
        {
            for (int x = bounds1.min.x; x < bounds1.max.x; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                tiles1[x - bounds1.min.x, y - bounds1.min.y] = map1.GetTile(pos);
                nullTiles[x - bounds1.min.x, y - bounds1.min.y] = null;
            }
        }
        BoundsInt playerPos =
            new(
                (int)Math.Floor(player.transform.position.x) - 5,
                (int)Math.Floor(player.transform.position.y) - 5,
                0,
                10,
                10,
                1
            );
        flattenedTiles = FlattenArray(tiles1);
        flattenedNullTiles = FlattenArray(nullTiles);
        map1.SetTilesBlock(bounds1, flattenedNullTiles);
        map1.SetTilesBlock(bounds1, flattenedTiles);
        // LoadChunk(map1, flattenedTiles, playerPos);
    }

    // Update is called once per frame
    void Update() { }

    private TileBase[] FlattenArray(TileBase[,] input)
    {
        int rows = input.GetLength(0);
        int cols = input.GetLength(1);
        TileBase[] output = new TileBase[rows * cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                output[i * cols + j] = input[i, j];
            }
        }
        return output;
    }

    private void LoadChunk(Tilemap tilemap, TileBase[] tiles, BoundsInt pos)
    {
        // Debug.Log(bounds1);
        TileBase[] chunkData = new TileBase[100];
        // int chunkSize = 10;
        // for (int i = 0; i < 10; i++)
        // {
        //     for (int j = 0; j < 10; j++)
        //     {
        //         chunkData[i - 1 * j] = tiles[
        //             Math.Abs(bounds1.xMax - bounds1.xMin) * Math.Abs(pos.y - bounds1.yMin) + i
        //                 + Math.Abs(pos.x - bounds1.xMin)
        //                 + j
        //         ];
        //     }
        // }
        int index = 0;
        for (int y = pos.y; y < pos.y + 10; y++)
        {
            for (int x = pos.x; x < pos.x + 10; x++)
            {
                int arrayIndex = y * (bounds1.xMax - bounds1.xMin + 1) + x;
                chunkData[index++] = tiles[arrayIndex];
            }
        }
        tilemap.SetTilesBlock(pos, chunkData);
    }
}
