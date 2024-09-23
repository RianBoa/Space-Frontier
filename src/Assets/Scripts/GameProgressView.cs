using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameProgressView : MonoBehaviour, IGameProgressView
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject victoryUI;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject pirateShipPrefab;
    [SerializeField] private Transform pirateSpawnPoint;
    private List<GameObject> asteroidPrefabs;
   
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Button restartButton;
   
    [SerializeField] private TextMeshProUGUI warningText;
    

    public void SpawnSpaceShip()
    {
        spaceShip.transform.position = SpawnPoint.transform.position;
        mainCamera.transform.parent = spaceShip.transform;
        spaceShip.SetActive(true);
    }
    public void ShowVictoryUI(string message)
    {
        statusText.text = message;
        victoryUI.SetActive(true);
    }

    public void ShowGameOverUI(string message)
    {
        statusText.text = message;
        gameOverUI.SetActive(true);
    }

    public void SpawnPirateShip()
    {
        pirateShipPrefab.SetActive(true);
        Debug.Log("Pirate ship has spawned!");
    }
    public void DestroyPirateShip()
    {
        pirateShipPrefab.SetActive(false);
    }

    public void SpawnAsteroids()
    {
      for (int i = 0; i < asteroidPrefabs.Count; i++)
        {
            asteroidPrefabs[i].gameObject.SetActive(true);
        }
    }
    public void ShowPiratesWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        Invoke(nameof(HideWarningText), 10f);
    }    
    public void ShowAsteroidsWarning(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        Invoke(nameof(HideWarningText), 10f);
    }
    public void ShowPiratesDefeated(string message)
    {
        warningText.text = message;
        warningText.gameObject.SetActive(true);
        Invoke(nameof(HideWarningText), 10f);
    }
        
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void HideWarningText()
    {
        warningText.gameObject.SetActive(false);
    }
    public void StartCoroutineBattle(IEnumerator couroutine)
    {
        StartCoroutine(couroutine);
    }
    public void SetAsteroid(List<GameObject > asteroids)
    { 
        asteroidPrefabs = asteroids;
    }
    
    
    
}
