using System;
using System.Collections.Generic;


public class Engine : IModule
{
    public string ModuleName { get; private set; } = "Engine";
    public ModuleType ModuleType =>  ModuleType.Engine;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int[] LevelPrices { get; private set; } = { 200, 300, 450 };
    public int[] DurabilityModifiers { get; private set; } = { -10, -8, -5 };

    

    public int[] EnergyConsumptionPerDistance { get; private set; } = { 50, 48, 45 };

    public int[] EnergyConsumptionPerBattle { get; private set; } = { 10, 8, 6 };

    public int[] AccelerationModifiers { get; private set; } = { 2, 4, 8 };

    public int[] SpeedModifier { get; private set; } = { 5, 10, 15 };

    public string ModuleId { get; private set; }

    public Engine()
    {
        ModuleId = GenerateUniqueId();
    }

   public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }
}