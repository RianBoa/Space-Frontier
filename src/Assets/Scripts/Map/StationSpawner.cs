using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSpawner : MonoBehaviour
{
    public event Action<Vector3> StationSpawned;

    [SerializeField] GameObject stationPrefab;
    [SerializeField] Camera mainCamera;
    [SerializeField] Transform spawnPoint; 

  public void StationSpawn(List<GameObject> Tiles)
    {
        int centerIndex = (MapSettings.scale.x / 2) * MapSettings.scale.x + (MapSettings.scale.y / 2);

        Vector3 centerPos = Tiles[centerIndex].transform.position;
        stationPrefab.transform.position = centerPos;

        mainCamera.transform.position = new Vector3(centerPos.x, centerPos.y, -20);
      
        var spawnPointPos = spawnPoint.transform.position;


    Tiles.Remove(Tiles[centerIndex]);

        StationSpawned?.Invoke(spawnPointPos);
    }
    
}
