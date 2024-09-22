using System;
using System.Collections.Generic;
using UnityEngine;

public interface IModule
{
    string ModuleName { get; }
    string ModuleId { get; }
    int[] LevelPrices { get; }
    int[] DurabilityModifiers { get; }
    int Level { get; set; }
    int MaxLevel { get; }

    ModuleType ModuleType { get; }

 

   public string GenerateUniqueId();
}
