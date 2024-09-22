using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Repairer : IModule
{
    public string ModuleName { get; private set; } = "Repairer";
    public ModuleType ModuleType => ModuleType.Repairer;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int[] LevelPrices { get; private set; } = { 350, 438, 547 };
    public int[] DurabilityModifiers { get; private set; } = { 10, 15, 20 };

   

    public int[] RepairRate { get; private set; } = { 20, 25, 30 }; // Скільки одиниць міцності відновлює за ремонт
    public int EnergyConsumptionPerRepair { get; private set; } = 10; // Скільки енергії споживає за ремонт

    public string ModuleId { get; private set; }
    public Repairer()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }
}