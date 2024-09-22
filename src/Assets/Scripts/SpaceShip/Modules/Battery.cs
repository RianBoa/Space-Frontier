using System;
using System.Collections.Generic;
using System.Reflection;


public class Battery : IModule
{
    public string ModuleName { get; private set; } = "Battery";
    public ModuleType ModuleType => ModuleType.Battery;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int[] LevelPrices { get; private set; } = { 150, 300, 450 };
    public int[] DurabilityModifiers { get; private set; } = { 10, 15, 20 };

  

    public int[] EnergyCapacityModifier { get; private set; } = { 1_000_000, 2_000_000, 3_000_000 };   
    public string ModuleId { get; private set; }

    public Battery()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }
}
