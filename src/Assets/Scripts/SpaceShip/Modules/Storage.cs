using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Storage : IModule
{
    public string ModuleName { get; private set; } = "Storage";
    public ModuleType ModuleType => ModuleType.Storage;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int [] LevelPrices { get; private set; } = { 50, 65, 85 };
    public int [] DurabilityModifiers { get; private set; } = { 10, 15, 20 };


    public int [] OreStorageCapacity { get; private set; } = { 2000, 3000, 4000 };

    public string ModuleId { get; private set; }
    public Storage()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }

}