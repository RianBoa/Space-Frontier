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

        // ������������� �� ������� View
       
        view.OnEnterResourceSource += DisplayResource;
        view.OnExitResourceSource += OnExit;

        // ������������� �� ������� ������
        model.OnStartRecharge += HandleStartRecharge;
    }

    private void HandleExitResourceSource()
    {
        // ������ ������ �� ���� (���� ���������, ��������, ������ ����������)
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
