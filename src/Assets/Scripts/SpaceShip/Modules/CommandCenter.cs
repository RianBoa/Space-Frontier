using System;
using System.Collections.Generic;
using System.Reflection;


public class CommandCenter :  IModule
{
    public string ModuleName { get; private set; } = "Command Center";
    public ModuleType ModuleType => ModuleType.CommandCenter;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int[] LevelPrices { get; private set; } = { 100, 300, 900 };
    public int[] DurabilityModifiers { get; private set; } = { 10, 20, 30 };

    public List<Hull> Hulls { get; private set; } = new List<Hull> { };

    public int[] MaxHulls { get; private set; } = { 4, 8 , 12 };

    public string ModuleId  { get; private set; }
    public CommandCenter()
    {
        ModuleId = GenerateUniqueId();
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }

}

