using UnityEngine;
using System.Collections;

public abstract class ResourceSourceBase : IResourceSource
{
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
        this.initialOre = initialOre;
        this.rechargeTime = rechargeTime;
        this.name = name;
        totalOre = initialOre;
    }

    public int ExtractOre(int amount)
    {
        if (isRecharging || isBeingExtracted == false)
        {
            return 0; // ���� ������ �� �����������, ������ �������� ����
        }

        int oreToExtract = System.Math.Min(amount, totalOre);
        totalOre -= oreToExtract;

        if (totalOre <= 0)
        {
            StartRecharge();
        }

        return oreToExtract;
    }

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
}
