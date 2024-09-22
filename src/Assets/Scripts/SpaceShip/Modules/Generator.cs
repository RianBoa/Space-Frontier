using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Generator :  IModule
{
    public string ModuleName { get; private set; } = "Generator";
    public ModuleType ModuleType => ModuleType.Generator;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int [] LevelPrices { get; private set; } = { 250, 388, 601 };
    public int [] DurabilityModifiers { get; private set; } = { 5, 8, 10 };

    

    public int[] EnergyGenerationModifier { get; private set; } = { 5, 10, 15 };
    
    public string ModuleId { get; private set; }
    public Generator()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }

}
