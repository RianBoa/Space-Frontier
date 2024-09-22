using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    public event Action OnPlanetSpawned;

    public PlanetPrefabCollection planetPrefabCollection;
    

    private List<string> availiableNames;
    private List<GameObject> availiablePlanets;


    private void Awake()
    {
       availiableNames = new List<string>(planetPrefabCollection.names);
       availiablePlanets = new List<GameObject>(planetPrefabCollection.planetPrefabs);
    }
    public void SpawnPlanets(List<GameObject> Tiles, int numberOfPlanets, Transform shipTransform)
    {
        
        if (Tiles.Count > 0 && availiablePlanets.Count > 0 && numberOfPlanets > 0)
        {
            for (int i = 0; i < numberOfPlanets; i++)
            {


                int randomSize = UnityEngine.Random.Range(2, 5);

                int randomTileIndex = UnityEngine.Random.Range(0, Tiles.Count);
                GameObject randomTile = Tiles[randomTileIndex];
             

                int randomPlanetIndex = UnityEngine.Random.Range(0, availiablePlanets.Count);
                GameObject planetPrefab = availiablePlanets[randomPlanetIndex];


                /* Vector3 randomOffSet = new Vector3(
                     UnityEngine.Random.Range(-MapSettings.tileSize / 2f, MapSettings.tileSize / 2f),
                     UnityEngine.Random.Range(-MapSettings.tileSize / 2f, MapSettings.tileSize / 2f),
                     0); */

                Vector3 planetPosition = randomTile.transform.position; // + randomOffSet;
                GameObject planet = Instantiate(planetPrefab, planetPosition, Quaternion.identity, randomTile.transform);
                planet.transform.localScale = new Vector3(randomSize, randomSize, randomSize); 
                Debug.Log($"Планета {planet.name} заспавнена на позиции {planetPosition} с масштабом {planet.transform.localScale}");

                int randomNameIndex = UnityEngine.Random.Range(0, availiableNames.Count);
                planet.name = availiableNames[randomNameIndex];

                availiableNames.RemoveAt(randomNameIndex);
                availiablePlanets.RemoveAt(randomPlanetIndex);
                Tiles.RemoveAt(randomTileIndex);

                CreatePlanetPresenter(planet, planet.name, shipTransform);
            }
        }

      

        OnPlanetSpawned?.Invoke();
    }
    private void CreatePlanetPresenter(GameObject planet, string name, Transform shipTransform)
    {
        // Создаем модель для планеты
        IResourceSource planetModel = new PlanetModel(1500, 300f, name); // Пример: 1000 единиц руды, перезарядка 300 секунд

        // Получаем View из планеты
        IResourceSourceView planetView = planet.GetComponent<IResourceSourceView>();

        if (planetView != null)
        {
            planetView.SetShipTransform(shipTransform);
            ResourceSourcePresenter planetPresenter = new ResourceSourcePresenter(planetView, planetModel);
            planetView.SetStatusText(statusText);
        }
        else
        {
            Debug.LogError($"Planet {planet.name} doesn't have a PlanetView component!");
        }
    }
}

