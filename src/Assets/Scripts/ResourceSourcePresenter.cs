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
       
        view.OnEnterResourceSource += OnEnter;
        view.OnExitResourceSource += OnExit;

        // ������������� �� ������� ������
        model.OnStartRecharge += HandleStartRecharge;
        view.OnEnterResourceSource += OnEnter;

        model.OnFinishRecharge +=
      
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
    private void OnEnter()
    {
        view.DisplayResourceInfo(model.GetResourceName(), model.OreAvailable);

    }
    private void OnExit()
    {

    }

}
