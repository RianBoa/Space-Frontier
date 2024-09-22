using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public event Action MapGenerated;

    [SerializeField] TileSpawner tileSpawner;
    [SerializeField] PlanetSpawner planetSpawner;
    [SerializeField] StationSpawner stationSpawner;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform SpaceShip;

    private void GenerateMap(Vector2Int scale, float TileSize, Transform parent)
    {
        tileSpawner.TilesSpawned += () => stationSpawner.StationSpawn(tileSpawner.spawnedTiles);
        stationSpawner.StationSpawned += position =>
        {
            spawnPoint.transform.position = position;  // Сохраняем позицию спауна
            planetSpawner.SpawnPlanets(tileSpawner.spawnedTiles, MapSettings.numberOfPlanets, SpaceShip);
        };
        planetSpawner.OnPlanetSpawned += () => MapGenerated?.Invoke();

        tileSpawner.SpawnTiles(scale, TileSize, parent);
    }
    public void MapInit()
    {
        GameObject map = new GameObject("Map");

        GenerateMap(MapSettings.scale, MapSettings.tileSize, map.transform);
    } 
    public void PlayerSpawnPoint(GameObject player)
    {
        player.transform.position = spawnPoint.position;
    }
}

