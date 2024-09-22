using System.Collections;

public class GameProgressPresenter
{
    private readonly IGameProgress gameProgress;
    private readonly IGameProgressView g_view;
    int piratesEncounter = 0;

    public GameProgressPresenter(IGameProgressView g_view, IGameProgress gameProgress)
    {
        this.gameProgress = gameProgress;
        this.g_view = g_view;

        gameProgress.OnPiratesAppeared += HandlePiratesAppeared;
        gameProgress.OnAsteroidsSpawned += HandleAsteroidsSpawned;
        gameProgress.OnVictory += HandleVictory;
        gameProgress.OnGameOver += HandleGameOver;
        gameProgress.ModulesPurchased += HandleModulesPurchased;
        gameProgress.OnPiratesDefeated += HandlePiratesDefeat;
    }

    private void HandlePiratesAppeared()
    {
        piratesEncounter++;
        // Вызываем отображение пиратов на экране через View
        g_view.SpawnPirateShip();
        PirateShip pirateShip = new PirateShip(100, 10, piratesEncounter);

        IEnumerator battleRoutine = gameProgress.StartBattle(pirateShip);
        g_view.StartCoroutineBattle(battleRoutine);
    }

    private void HandleAsteroidsSpawned()
    {
        // Спавн астероидов через View
        g_view.SpawnAsteroids();
    }

    private void HandleVictory()
    {
        // Показываем UI победы
        g_view.ShowVictoryUI("Victory! You collected enough crypto!");
    }

    private void HandleGameOver()
    {
        // Показываем UI поражения
        g_view.ShowGameOverUI("Game Over! You lost the battle!");
    }

  
    private void HandleModulesPurchased()
    {
        g_view.SpawnSpaceShip();
    }
    private void HandlePiratesDefeat()
    {
        g_view.ShowPiratesDefeated("Congratulations! You defeated a pirate ship, you earn an award!");
        g_view.DestroyPirateShip();
    }
}
    
