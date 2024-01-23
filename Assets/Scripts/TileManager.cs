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
    private TileBase[] tiles1;
    private TileBase[] flattenedTiles;
    private TileBase[] nullTiles;
    private TileBase[] flattenedNullTiles;

    [SerializeField]
    private TileBase testTile;
    private int chunkSize = 10;

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
        tiles1 = new TileBase[bounds1.size.x * bounds1.size.y];
        nullTiles = new TileBase[bounds1.size.x * bounds1.size.y];

        // tiles2 = new TileBase[bounds2.size.x, bounds2.size.y];
        // tiles3 = new TileBase[bounds3.size.x, bounds3.size.y];
        // tiles4 = new TileBase[bounds4.size.x, bounds4.size.y];
        int boundWidth = bounds1.xMax - bounds1.xMin;
        int boundHeight = bounds1.yMax - bounds1.yMin;
        Debug.Log(boundWidth);
        for (int y = bounds1.min.y; y < bounds1.max.y; y++)
        {
            for (int x = bounds1.min.x; x < bounds1.max.x; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                tiles1[x - bounds1.min.x + ((y - bounds1.min.y) * boundWidth)] = map1.GetTile(pos);
                nullTiles[x - bounds1.min.x + ((y - bounds1.min.y) * boundWidth)] = null;
                // nullTiles[x - bounds1.min.x, y - bounds1.min.y] = null;
            }
        }
        BoundsInt playerPos =
            new(
                (int)Math.Floor(player.transform.position.x) - 5,
                (int)Math.Floor(player.transform.position.y) - 5,
                0,
                chunkSize,
                chunkSize,
                1
            );
        BoundsInt testPos = new(bounds1.xMin, bounds1.yMin - 5, 0, boundWidth, boundHeight, 1);
        // flattenedTiles = FlattenArray(tiles1);
        // flattenedNullTiles = FlattenArray(nullTiles);
        map1.SetTilesBlock(bounds1, nullTiles);
        // map1.SetTilesBlock(bounds1, tiles1);
        LoadChunk(boundWidth, map1, tiles1, playerPos);
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

    private void LoadChunk(int width, Tilemap tilemap, TileBase[] tiles, BoundsInt pos)
    {
        TileBase[] chunkData = new TileBase[chunkSize * chunkSize];
        int index = 0;
        for (int i = 0; i < chunkSize; i++)
        {
            for (int j = 0; j < chunkSize; j++)
            {
                chunkData[index] = tiles[
                    width * Math.Abs(pos.y + i - bounds1.yMin) + Math.Abs(pos.x - bounds1.xMin) + j
                ];
                index++;
            }
        }
        tilemap.SetTilesBlock(pos, chunkData);
    }
}

//big brain solution (in typescript)
// type Grid<T> = {
//   dim: {
//     xMin: number;
//     xMax: number;
//     yMin: number;
//     yMax: number;
//   },
//   cells: T[]
// };


// type Coord = {
//   posX: number;
//   posY: number;
// }

// const get_rekt = <T>(grid: Grid<T>, bottom_left: Coord, chunksize: number ) => {
//   const stride = (grid.dim.xMax - grid.dim.xMin) + 1
//   let src_index = bottom_left.posX + bottom_left.posY * stride;
//   // allocate result array;
//   const result = Array.from({length: chunksize * chunksize});
//   let dst_index = 0;
//   for (let y = 0; y < chunksize; y++) {
//     for(let x = 0; x < chunksize; x++) {
//       result[dst_index++] = grid.cells[src_index++];
//     }
//     src_index += stride - chunksize;
//   }
//   return result;
// }
