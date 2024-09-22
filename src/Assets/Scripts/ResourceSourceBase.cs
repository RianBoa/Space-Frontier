using UnityEngine;
using System.Collections;

public abstract class ResourceSourceBase : IResourceSource
{
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
        this.initialOre = initialOre;
        this.rechargeTime = rechargeTime;
        this.name = name;
        totalOre = initialOre;
    }

    public int ExtractOre(int amount)
    {
        if (isRecharging || isBeingExtracted == false)
        {
            return 0; // Если ресурс на перезарядке, нельзя собирать руду
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
}
