using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText;
    public event Action OnPlanetSpawned;
    public event Action<List<GameObject>> OnAsteroidsInit;

    public PlanetPrefabCollection planetPrefabCollection;
    

    private List<string> availiableNames;
    private List<GameObject> availiablePlanets;
    [SerializeField] public List<GameObject> asteroids;


    private void Awake()
    {
       availiableNames = new List<string>(planetPrefabCollection.names);
       availiablePlanets = new List<GameObject>(planetPrefabCollection.planetPrefabs);
    }
    public void SpawnPlanets(List<GameObject> Tiles, int numberOfPlanets, int numberOfAsteroids)
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




                int randomNameIndex = UnityEngine.Random.Range(0, availiableNames.Count);
                planet.name = availiableNames[randomNameIndex];

                availiableNames.RemoveAt(randomNameIndex);
                availiablePlanets.RemoveAt(randomPlanetIndex);
                Tiles.RemoveAt(randomTileIndex);

                CreatePlanetPresenter(planet, planet.name);


            }
            if (Tiles.Count > 0 && asteroids.Count > 0 && numberOfAsteroids > 0)
            {
                for (int i = 0; i < numberOfAsteroids; i++)
                {
                    int randomSize = UnityEngine.Random.Range(2, 4); // Размер астероидов может быть меньше
                    int randomTileIndex = UnityEngine.Random.Range(0, Tiles.Count);
                    GameObject randomTile = Tiles[randomTileIndex];

                    int randomAsteroidIndex = UnityEngine.Random.Range(0, asteroids.Count);
                    GameObject asteroidPrefab = asteroids[randomAsteroidIndex];

                    Vector3 asteroidPosition = randomTile.transform.position;
                    GameObject asteroid = Instantiate(asteroidPrefab, asteroidPosition, Quaternion.identity, randomTile.transform);
                    asteroid.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                    asteroid.SetActive(false); // Астероид по умолчанию неактивен

                    int randomNameIndex = UnityEngine.Random.Range(0, availiableNames.Count);
                    asteroid.name = availiableNames[randomNameIndex];

                    availiableNames.RemoveAt(randomNameIndex);
                    availiableNames.RemoveAt(randomAsteroidIndex);
                    Tiles.RemoveAt(randomTileIndex);

                    CreateAsteroid(asteroid, asteroid.name);
                }



                OnPlanetSpawned?.Invoke();
                OnAsteroidsInit?.Invoke(asteroids);
            }
        }    }
    private void CreatePlanetPresenter(GameObject planet, string name)
    {
        // Создаем модель для планеты
        IResourceSource planetModel = new PlanetModel(2500, 120f, name); // Пример: 1000 единиц руды, перезарядка 300 секунд

        // Получаем View из планеты
        IResourceSourceView planetView = planet.GetComponent<IResourceSourceView>();

        ResourceSourceIdCollection.AddResource(planetModel.GetId(), planetModel);


        if (planetView != null)
        {
          
            ResourceSourcePresenter planetPresenter = new ResourceSourcePresenter(planetView, planetModel);
            planetView.SetStatusText(statusText);
            planetView.Id = planetModel.GetId();
        }

    }
    private void CreateAsteroid(GameObject asteroid, string name)
    {
      

    IResourceSource asteroidModel = new AsteroidModel(1000, 120f, name); // Пример: 1000 единиц руды, перезарядка 300 секунд

    // Получаем View из планеты
    IResourceSourceView asteroidView = asteroid.GetComponent<IResourceSourceView>();

    ResourceSourceIdCollection.AddResource(asteroidModel.GetId(), asteroidModel);


        if (asteroidView != null)
        {
            
            ResourceSourcePresenter asteroidPresenter = new ResourceSourcePresenter(asteroidView, asteroidModel);
            asteroidView.SetStatusText(statusText);
            asteroidView.Id = asteroidModel.GetId();
        }
    }

}
 

