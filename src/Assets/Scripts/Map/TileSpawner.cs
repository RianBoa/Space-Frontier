using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public event Action TilesSpawned; 

    [SerializeField] private GameObject tilePrefab;

    public List<GameObject> spawnedTiles = new List<GameObject>();
 
    public void SpawnTiles(Vector2Int scale, float TileSize, Transform parent)
    {
        for (int i = 0; i < scale.x; i++)
        {
            for (int j = 0; j < scale.y; j++)
            {
                Vector3 position = new Vector3(i * TileSize, j * TileSize, 0);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, parent);
                tile.transform.parent = parent;
                tile.name = $"Space {i}, {j}";
                spawnedTiles.Add(tile);
            }
        }
        TilesSpawned?.Invoke();
    }
}
