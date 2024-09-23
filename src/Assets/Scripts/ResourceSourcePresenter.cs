using System.Runtime.CompilerServices;
using UnityEngine;

public class ResourceSourcePresenter
{
    private readonly IResourceSource model;
    private readonly IResourceSourceView view;

    public ResourceSourcePresenter(IResourceSourceView view, IResourceSource model)
    {
        this.view = view;
        this.model = model;

        // Подписываемся на события View
       
        view.OnEnterResourceSource += DisplayResource;
        view.OnExitResourceSource += OnExit;

        // Подписываемся на события модели
        model.OnStartRecharge += HandleStartRecharge;
    }

    private void HandleExitResourceSource()
    {
        // Логика выхода из зоны (если требуется, например, скрыть информацию)
    }

    private void HandleStartRecharge()
    {

        view.StartRechargeTimer(model.StartRecharge());
    }
    private void HandleFinishRecharge()
    {
        
    }
    private void DisplayResource()
    {
        view.DisplayResourceInfo(model.GetResourceName(), model.OreAvailable);

    }
    private void OnExit()
    {

    }

}
