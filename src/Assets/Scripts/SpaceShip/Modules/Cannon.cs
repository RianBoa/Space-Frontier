using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Cannon : IModule
{
    public string ModuleName { get; private set; } = "Cannon";
    public ModuleType ModuleType => ModuleType.Cannon;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int [] LevelPrices { get; private set; } = { 150, 270, 486 };
    public int [] DurabilityModifiers { get; private set; } = { -5, -3, -1 };

    public int [] LevelDamageMultiplier { get; private set; } = { 50, 60, 70, };

    public int  EnergyComsumptionPerShot { get; private set; } = 5;

    public string ModuleId { get; private set; }
    public Cannon()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }

}
