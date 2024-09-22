using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{ 

   [SerializeField] private MapGenerator mapGenerator;

   [SerializeField] private GameObject spaceShipPrefab;

   [SerializeField] private GameObject player;

   


    void Start()
    {
        mapGenerator.MapInit();
        mapGenerator.PlayerSpawnPoint(player);
        

        

        // Подписка на событие покупки всех необходимых модулей

    }

    private void ShipInit()
    {
      spaceShipPrefab.SetActive(true);
      mapGenerator.PlayerSpawnPoint(spaceShipPrefab);
        player.SetActive(false);
    }

    

}
