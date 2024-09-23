using System.Collections;
using UnityEngine;

public class GameProgress : IGameProgress
{
    private IResourceManager resourceManager;
    private ShipModuleStats shipModuleStats;
    private BattleManager battleManager;
    private ModuleHandleController moduleHandleController;

    private bool piratesSpawned = false;
    private bool asteroidsSpawned = false;

    private const int requiredCollections = 100;
    public int collectedOre { get; set; } = 0;
    private const int requiredCryptoForVictory = 5000;

    public event System.Action OnPiratesAppeared;
    public event System.Action OnAsteroidsSpawned;
    public event System.Action OnGameOver;
    public event System.Action OnVictory;
    public event System.Action ModulesPurchased;
    public event System.Action OnPiratesDefeated;

    public GameProgress(IResourceManager resourceManager, ShipModuleStats shipModuleStats, BattleManager battleManager, ModuleHandleController moduleHandleController)
    {
        this.resourceManager = resourceManager;
        this.shipModuleStats = shipModuleStats;
        this.battleManager = battleManager;
        this.moduleHandleController = moduleHandleController;
        battleManager.OnBattleEnded += HandleBattleEnded; // Подписка на завершение боя
        moduleHandleController.AllModulesPurchased += OnAllModulesPurchased;
    }

    public void CollectResourcesFromPlanet()
    {
        if (collectedOre >= requiredCollections)
        {
            if (!asteroidsSpawned)
            {
                asteroidsSpawned = true;
                OnAsteroidsSpawned?.Invoke(); // Вызов события появления астероидов
            }
        }
    }

    public IEnumerator StartBattle(PirateShip pirateShip)
    {
       return battleManager.StartBattle(resourceManager, pirateShip);
    }

    public void HandleBattleEnded(bool playerWon)
    {
        if (CheckDefeatCondition())
            playerWon = false;
        else
        {
            playerWon = true;
            resourceManager.AddResource(ResourceType.Ore, 500);
            resourceManager.AddResource(ResourceType.Energy, 1500);
            OnPiratesDefeated?.Invoke(); 
        }
    }
    public void HandleColliderExit()
    {
        if (!piratesSpawned && Random.Range(0, 100) < 50 && collectedOre == requiredCollections) // 50% шанс
        {
            piratesSpawned = true;
            OnPiratesAppeared?.Invoke(); // Вызов события появления пиратов
        }
    }

    public void CheckVictoryCondition()
    {
        if (resourceManager.GetResourceAmount(ResourceType.Crypto) >= requiredCryptoForVictory)
        {
            OnVictory?.Invoke(); // Победа
        }
    }

    public bool CheckDefeatCondition()
    {
        if (shipModuleStats.CurrentDurability <= 0 || resourceManager.GetResourceAmount(ResourceType.Energy) <= 0)
        {
            OnGameOver?.Invoke(); // Поражение
            return true;
        }
        return false;
    }
    public void AddCollectedOre(int oreAmount)
    {
        collectedOre += oreAmount;
    }
    public void OnAllModulesPurchased()
    {
       ModulesPurchased?.Invoke(); 
    }
    public int CollectedOre()
    {
        return collectedOre;
    }

}
