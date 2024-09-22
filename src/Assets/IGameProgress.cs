using System;
using System.Collections;

public interface IGameProgress
{
    public int collectedOre { get; set; }  
    public void CheckVictoryCondition();
    public void AddCollectedOre(int oreAmount);
  public bool CheckDefeatCondition();
    public void OnAllModulesPurchased();
    public void CollectResourcesFromPlanet();
    public void HandleColliderExit();
    public IEnumerator StartBattle(PirateShip pirateShip);

    public event Action OnPiratesAppeared;
    public event Action OnAsteroidsSpawned;
    public event Action OnGameOver;
    public event Action OnVictory;
    public event Action ModulesPurchased;
    public event Action OnPiratesDefeated;

}
