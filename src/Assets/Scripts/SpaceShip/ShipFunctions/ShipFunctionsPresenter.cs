using System.Diagnostics;

public class ShipFunctionsPresenter
{
    private readonly IShipFunctionsModel model;
    private readonly IShipFunctionsView view;
    private readonly IGameProgress gameProgress;

    public ShipFunctionsPresenter(IShipFunctionsView view, IShipFunctionsModel model, IGameProgress gameProgress)
    {
        this.view = view;
        this.model = model;
        this.gameProgress = gameProgress;

        // Подписываемся на события от View
        this.view.OnEnterResourceSource += HandleEnterResourceSource;
        this.view.OnExitResourceSource += HandleExitResourceSource;
        this.view.OnExitPlanetCollider += HandleExitPlanetCollider;
        this.view.OnStartOreCollection += HandleStartOreCollection;
        this.view.OnStartRepair += HandleStartRepair;
        this.model.onError += HandleError;
    }

    private void HandleEnterResourceSource(string Id)
    {
       
        var resource = ResourceSourceIdCollection.GetResourceById(Id);
        if (resource != null)
        {
            model.SetCurrentResourceSource(resource);
            view.ShowMessage($"Planet id is {resource.GetId()}");
            UnityEngine.Debug.Log("Planet ore " + resource.OreAvailable);
        }
        else
        {
            view.ShowError("Resource not found");
        }
    }

    // Обработка выхода из зоны с ресурсами
    private void HandleExitResourceSource()
    {
        var currentResourceSource = model.CurrentResourceSource; // Получаем текущий источник ресурсов

        if (currentResourceSource != null)
        {
            model.ClearCurrentResourceSource(currentResourceSource); // Очищаем текущий ресурс
        }
    }

    private void HandleExitPlanetCollider()
    {
        gameProgress.CollectResourcesFromPlanet();
        gameProgress.HandleColliderExit();
    }

    private void HandleStartOreCollection()
    {
        if (model.CurrentResourceSource != null)
        {
            // Запускаем корутину через View
            view.StartCustomCoroutine(model.CollectOreCoroutine(model.CurrentResourceSource, AddCollectedOre));
          
            view.ShowMessage("Collected ore" + gameProgress.CollectedOre().ToString());
        }
        else
        {
            view.ShowError("No resource source selected.");
        }
    }
    private void AddCollectedOre(int oreAmount)
    {
        gameProgress.AddCollectedOre(oreAmount); 
    }

    private void HandleStartRepair()
    {
        // Запускаем корутину ремонта через View
        view.StartCustomCoroutine(model.RepairCoroutine(
            repairProgress => view.ShowMessage($"Repair Progress: {repairProgress}"),
            error => view.ShowError(error)
        ));
    }
    private void HandleError(string message)
    {
        view.ShowError(message);
    }


}
