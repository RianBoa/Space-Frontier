using System;
using System.Collections.Generic;

public static class ModuleInfo
{

    public static Dictionary<ModuleType, string> ModuleNames = new Dictionary<ModuleType, string>()
    {
        { ModuleType.CommandCenter, "Command Center" },
        { ModuleType.Hull, "Hull" },
        { ModuleType.Engine, "Engine" },
        { ModuleType.Battery, "Battery" },
        { ModuleType.Storage, "Storage" },
        { ModuleType.Cannon, "Cannon" },
        { ModuleType.Collector, "Collector" },
        { ModuleType.Converter, "Converter" },
        { ModuleType.Generator, "Generator" },
        { ModuleType.Repairer, "Repairer" }
    };

   
    public static Dictionary<ModuleType, int[]> ModulePrices = new Dictionary<ModuleType, int[]>()
    {
        { ModuleType.CommandCenter, new int[] { 100, 300, 900 } },
        { ModuleType.Hull, new int[] { 100, 250, 625 } },
        { ModuleType.Engine, new int[] { 200, 300, 450 } },
        { ModuleType.Battery, new int[] { 150, 300, 450 } },
        { ModuleType.Storage, new int[] { 50, 65, 85 } },
        { ModuleType.Cannon, new int[] { 150, 270, 486 } },
        { ModuleType.Collector, new int[] { 75, 131, 230 } },
        { ModuleType.Converter, new int[] { 200, 270, 365 } },
        { ModuleType.Generator, new int[] { 250, 388, 601 } },
        { ModuleType.Repairer, new int[] { 350, 438, 547 } }
    };
    public static Dictionary<ModuleType, List<ModuleType>> IncompatibleModules = new Dictionary<ModuleType, List<ModuleType>>()
{
    { ModuleType.CommandCenter, new List<ModuleType> { ModuleType.Engine, ModuleType.Cannon, ModuleType.Storage } },
    { ModuleType.Engine, new List<ModuleType> { ModuleType.Cannon, ModuleType.Converter } },
    { ModuleType.Hull, new List<ModuleType>() }, // Корпус может быть совместим со всеми
    { ModuleType.Battery, new List<ModuleType> { ModuleType.Storage } },
    { ModuleType.Storage, new List<ModuleType> { ModuleType.Battery } }, // Хранилище не совместимо с Аккумулятором
    { ModuleType.Cannon, new List<ModuleType> { ModuleType.Engine, ModuleType.Converter, ModuleType.Generator } },
    { ModuleType.Collector, new List<ModuleType> { ModuleType.Engine, ModuleType.Cannon } },
    { ModuleType.Converter, new List<ModuleType> { ModuleType.Engine, ModuleType.Cannon } },
    { ModuleType.Generator, new List<ModuleType> { ModuleType.Cannon } },
    { ModuleType.Repairer, new List<ModuleType>() } // Ремонтник может быть совместим со всеми
};


    public static string GetModuleName(ModuleType moduleType)
    {
        if (ModuleNames.ContainsKey(moduleType))
        {
            return ModuleNames[moduleType];
        }
        return "Unknown Module";
    }
    public static bool IsCompatible(ModuleType moduleType, ModuleType adjacentModuleType)
    {
        if (IncompatibleModules.ContainsKey(moduleType))
        {
            return !IncompatibleModules[moduleType].Contains(adjacentModuleType);
        }
        return true;
    }
  
    public static int GetPriceForLevel(ModuleType moduleType, int level)
    {
        if (ModulePrices.ContainsKey(moduleType))
        {
            return ModulePrices[moduleType][level - 1]; // Уровень начинается с 1, индексы массива с 0
        }
        return 0; // Если модуль не найден
    }
}
