using UnityEngine;
using System.Collections;
using System;

public abstract class ResourceSourceBase : IResourceSource
{
    private string Id;
    protected string name;
    private int initialOre;
    protected int totalOre; // ����� ���������� ����
    protected float rechargeTime; // ����� �����������
    protected bool isRecharging = false; // ��������� �����������
    protected bool isBeingExtracted;

    public event System.Action OnStartRecharge; // ������� ������ �����������
    public event System.Action OnFinishRecharge;

    public ResourceSourceBase(int initialOre, float rechargeTime, string name)
    {
        Id = GenerateId();
        this.initialOre = initialOre;
        this.rechargeTime = rechargeTime;
        this.name = name;
        totalOre = initialOre;
    }

    public int ExtractOre(int amount)
    {
        if (isRecharging || isBeingExtracted == false)
        {
            Debug.LogError("Cannot extract ore: either the resource is recharging or extraction hasn't started.");
            return 0; // ���� ������ �� ����������� ��� ������ �� ��������, ������ �������� ����
      // ���� ������ �� �����������, ������ �������� ����
        }

        int oreToExtract = UnityEngine.Mathf.Min(amount, totalOre);
        totalOre -= oreToExtract;
     

        if (totalOre <= 0)
        {
            StartRecharge();
        }

        return oreToExtract;
    }

    private string GenerateId()
    {
        return Guid.NewGuid().ToString();  
    }
    public string GetId()
        { return Id; }  
    public bool IsDepleted()
    {
        return totalOre <= 0 && isRecharging;
    }

    public int OreAvailable => totalOre;

    public string GetResourceName()
    {
        return name;
    }

    public IEnumerator StartRecharge()
    {
        if (!isRecharging)
        {
            isRecharging = true;
            OnStartRecharge?.Invoke(); // ���������� �����������, ��� ������ ��������������

            // ���� ��������� �����
            yield return new WaitForSeconds(rechargeTime);

            FinishRecharge(); // ��������� �����������
        }
    }


    public void FinishRecharge()
    {
        totalOre = GetInitialOreAmount(); // ��������������� ���������� ����
        isRecharging = false;
        OnFinishRecharge?.Invoke(); // ���������� �����������, ��� ����������� ���������
    }
    public void StartExtraction()
    {
        isBeingExtracted = true;
    }

    public void StopExtraction()
    {
        isBeingExtracted = false;
    }
    private int GetInitialOreAmount()
    {
        return initialOre;
    }
    public int GetAvailableOre()
    {
        return totalOre;
    }
}
