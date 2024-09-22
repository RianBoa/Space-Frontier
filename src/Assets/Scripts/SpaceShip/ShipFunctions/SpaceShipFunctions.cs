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
        // Проверяем, достаточно ли энергии для начала сбора руды
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
        int steps = 30; // Количество шагов за 3 секунды (1 шаг = 0.1 секунды)
        int orePerStep = totalOreToCollect / steps;
        int energyPerStep = shipStats.CurrentEnergyConsumptionPerCollection / steps;

        // Плавный процесс сбора руды
        for (int i = 0; i < steps; i++)
        {
            // Собираем небольшую часть руды и уменьшаем энергию
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
                    yield break; // Прекращаем сбор, если больше нет руды
                }
            }
            else
            {
                onError?.Invoke("Not enough energy to continue collecting ore.");
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }

        // Завершающий шаг, если осталось немного руды
        int remainingOre = totalOreToCollect % steps;
        if (remainingOre > 0)
        {
            shipStats.CurrentOreStorageCapacity -= remainingOre;
            onOreCollected?.Invoke(remainingOre); // Последнее обновление
        }
    }


    public IEnumerator RepairCoroutine(Action<int> onRepairProgress, Action<string> onError)
    {
        if (shipStats.CurrentDurability == shipStats.BaseDurability)
        {
            onError.Invoke("You are fully repaired");
            yield break;
        }
        // Проверяем, достаточно ли энергии для начала ремонта
        if (resourceManager.GetResourceAmount(ResourceType.Energy) < shipStats.CurrentEnergyConsumptionPerRepair)
        {
            onError?.Invoke("Not enough energy to repair the ship.");
            yield break; // Выход из корутины, если недостаточно энергии
        }

        int totalRepairAmount = shipStats.BaseDurability - shipStats.CurrentDurability;
        int steps = 30; // Количество шагов за 3 секунды (1 шаг = 0.1 секунды)
        int repairPerStep = Mathf.Min(totalRepairAmount / steps, shipStats.CurrentRepairRate);
        int energyPerStep = shipStats.CurrentEnergyConsumptionPerRepair / steps;

        // Плавный процесс починки
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

                onRepairProgress?.Invoke(repairPerStep); // Обновляем состояние прочности плавно
            }
            else
            {
                onError?.Invoke("Not enough energy to continue repair.");
                yield break; // Прекращаем ремонт, если недостаточно энергии
            }

            yield return new WaitForSeconds(0.1f); // Плавное выполнение каждые 0.1 секунды
        }

        // Завершающий шаг, если осталось немного для полной починки
        int remainingRepair = totalRepairAmount % steps;
        if (remainingRepair > 0)
        {
            shipStats.CurrentDurability += remainingRepair;

            // Не превышаем базовое значение прочности
            if (shipStats.CurrentDurability > shipStats.BaseDurability)
            {
                shipStats.CurrentDurability = shipStats.BaseDurability;
            }

            onRepairProgress?.Invoke(remainingRepair); // Последнее обновление прочности
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
    
        // Дополнительная логика при выходе из коллайдера
    }
}
