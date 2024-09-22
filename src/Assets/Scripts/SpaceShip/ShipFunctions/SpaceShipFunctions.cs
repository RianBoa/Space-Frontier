using System;
using System.Collections;
using UnityEngine;

public class ShipFunctionsModel : IShipFunctionsModel
{
    private ShipModuleStats shipStats;
    private IResourceManager resourceManager;
    private IResourceSource currentResourceSource;

    public IResourceSource CurrentResourceSource
    { 
        get => currentResourceSource;
        set => currentResourceSource = value;
    }

    
    public event Action<string> onError;
    public ShipFunctionsModel(ShipModuleStats shipStats, IResourceManager resourceManager)
    {
        this.shipStats = shipStats;
        this.resourceManager = resourceManager;
    }

    public IEnumerator CollectOreCoroutine(IResourceSource resourceSource, Action<int> onOreCollected)
    {
        if(resourceManager.GetResourceAmount(ResourceType.Ore) == shipStats.CurrentOreStorageCapacity)
        {
            onError.Invoke("Your storage is full!");
            yield break;
        }
        // ���������, ���������� �� ������� ��� ������ ����� ����
        if (resourceManager.GetResourceAmount(ResourceType.Energy) < shipStats.CurrentEnergyConsumptionPerCollection)
        {
            onError?.Invoke("Not enough energy to collect ore.");
            yield break; 
        }
        if (resourceSource.IsDepleted())
        {
            onError.Invoke("Currently is depleted");
            yield break;
        }
        int oreCollectionRate = shipStats.CurrentOreCollectionRate;
        int totalOreToCollect = Mathf.Min(oreCollectionRate, shipStats.CurrentOreStorageCapacity);
        int steps = 30; // ���������� ����� �� 3 ������� (1 ��� = 0.1 �������)
        int orePerStep = totalOreToCollect / steps;
        int energyPerStep = shipStats.CurrentEnergyConsumptionPerCollection / steps;

        // ������� ������� ����� ����
        for (int i = 0; i < steps; i++)
        {
            // �������� ��������� ����� ���� � ��������� �������
            if (resourceManager.GetResourceAmount(ResourceType.Energy) >= energyPerStep)
            {
                resourceManager.SpendResource(ResourceType.Energy, energyPerStep);
                int collectedOre = resourceSource.ExtractOre(orePerStep);

                if (collectedOre > 0)
                {
                    shipStats.CurrentOreStorageCapacity -= orePerStep;
                    onOreCollected?.Invoke(orePerStep);
                }
                else
                {
                    onError?.Invoke("No more ore available.");
                    yield break; // ���������� ����, ���� ������ ��� ����
                }
            }
            else
            {
                onError?.Invoke("Not enough energy to continue collecting ore.");
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        // ����������� ���, ���� �������� ������� ����
        int remainingOre = totalOreToCollect % steps;
        if (remainingOre > 0)
        {
            shipStats.CurrentOreStorageCapacity -= remainingOre;
            onOreCollected?.Invoke(remainingOre); // ��������� ����������
        }
    }


    public IEnumerator RepairCoroutine(Action<int> onRepairProgress, Action<string> onError)
    {
        if (shipStats.CurrentDurability == shipStats.BaseDurability)
        {
            onError.Invoke("You are fully repaired");
            yield break;
        }
        // ���������, ���������� �� ������� ��� ������ �������
        if (resourceManager.GetResourceAmount(ResourceType.Energy) < shipStats.CurrentEnergyConsumptionPerRepair)
        {
            onError?.Invoke("Not enough energy to repair the ship.");
            yield break; // ����� �� ��������, ���� ������������ �������
        }

        int totalRepairAmount = shipStats.BaseDurability - shipStats.CurrentDurability;
        int steps = 30; // ���������� ����� �� 3 ������� (1 ��� = 0.1 �������)
        int repairPerStep = Mathf.Min(totalRepairAmount / steps, shipStats.CurrentRepairRate);
        int energyPerStep = shipStats.CurrentEnergyConsumptionPerRepair / steps;

        // ������� ������� �������
        for (int i = 0; i < steps; i++)
        {
           
            if (resourceManager.GetResourceAmount(ResourceType.Energy) >= energyPerStep)
            {
                resourceManager.SpendResource(ResourceType.Energy, energyPerStep);
                shipStats.CurrentDurability += repairPerStep;

                
                if (shipStats.CurrentDurability > shipStats.BaseDurability)
                {
                    shipStats.CurrentDurability = shipStats.BaseDurability;
                }

                onRepairProgress?.Invoke(repairPerStep); // ��������� ��������� ��������� ������
            }
            else
            {
                onError?.Invoke("Not enough energy to continue repair.");
                yield break; // ���������� ������, ���� ������������ �������
            }

            yield return new WaitForSeconds(0.1f); // ������� ���������� ������ 0.1 �������
        }

        // ����������� ���, ���� �������� ������� ��� ������ �������
        int remainingRepair = totalRepairAmount % steps;
        if (remainingRepair > 0)
        {
            shipStats.CurrentDurability += remainingRepair;

            // �� ��������� ������� �������� ���������
            if (shipStats.CurrentDurability > shipStats.BaseDurability)
            {
                shipStats.CurrentDurability = shipStats.BaseDurability;
            }

            onRepairProgress?.Invoke(remainingRepair); // ��������� ���������� ���������
        }
    }
    public void SetCurrentResourceSource(IResourceSource resourceSource)
    {
        currentResourceSource = resourceSource;
    }
    public void ClearCurrentResourceSource(IResourceSource resourceSource)
    {
        CurrentResourceSource = null;
    }



    public void OnLeaveCollider()
    {
    
        // �������������� ������ ��� ������ �� ����������
    }
}
