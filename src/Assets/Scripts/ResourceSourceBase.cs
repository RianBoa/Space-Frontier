using UnityEngine;
using System.Collections;
using System;

public abstract class ResourceSourceBase : IResourceSource
{
    private string Id;
    protected string name;
    private int initialOre;
    protected int totalOre; // Общее количество руды
    protected float rechargeTime; // Время перезарядки
    protected bool isRecharging = false; // Состояние перезарядки
    protected bool isBeingExtracted;

    public event System.Action OnStartRecharge; // Событие начала перезарядки
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
            return 0; // Если ресурс на перезарядке или добыча не началась, нельзя собирать руду
      // Если ресурс на перезарядке, нельзя собирать руду
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
            OnStartRecharge?.Invoke(); // Уведомляем подписчиков, что ресурс перезаряжается

            // Ждем указанное время
            yield return new WaitForSeconds(rechargeTime);

            FinishRecharge(); // Завершаем перезарядку
        }
    }


    public void FinishRecharge()
    {
        totalOre = GetInitialOreAmount(); // Восстанавливаем количество руды
        isRecharging = false;
        OnFinishRecharge?.Invoke(); // Уведомляем подписчиков, что перезарядка завершена
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
