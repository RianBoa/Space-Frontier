using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Collector : IModule
{
    public string ModuleName { get; private set; } = "Collector";
    public ModuleType ModuleType => ModuleType.Collector;

    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int [] LevelPrices { get; private set; } = { 75, 131, 230 };
    public int [] DurabilityModifiers { get; private set; } = { 10, 12, 15 };

    public int[] CollectRateModifier { get; private set; } = { 20, 30, 40 };

    public int EnergyConsumptionPerCollect { get; private set; } = 10;

    public string ModuleId { get; private set; }
    public Collector()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }

}
