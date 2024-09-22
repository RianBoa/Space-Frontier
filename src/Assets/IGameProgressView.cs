using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameProgressView
{
    public void SpawnAsteroids();
    public void SpawnPirateShip();
    public void DestroyPirateShip();
    public void ShowPiratesWarning(string message);
    public void ShowAsteroidsWarning(string message);
    public void ShowGameOverUI(string message);
    public void ShowPiratesDefeated(string message);
    public void ShowVictoryUI(string message);
    public void SpawnSpaceShip();

    public void StartCoroutineBattle(IEnumerator couroutine);
}
