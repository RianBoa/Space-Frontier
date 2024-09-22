using System;
using System.Collections.Generic;
using System.Reflection;


public class Converter : IModule
{
    public string ModuleName { get; private set; } = "Converter";
    public ModuleType ModuleType => ModuleType.Converter;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int[] LevelPrices { get; private set; } = { 200, 270, 365 };
    public int[] DurabilityModifiers { get; private set; } = { -5, -3, 0 };

    
    public int[] OreConverterRate { get; private set; } = { 5, 4, 3 };
    public int EnergyConsumptionPerOre = 1;

    public string ModuleId { get; private set; }
    public Converter()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }
}
