using System;
using System.Collections;
using System.Collections.Generic;


public class Hull : IModule
{
    public string ModuleName { get; private set; } = "Hull";
    public ModuleType ModuleType => ModuleType.Hull;
    public int Level { get; set; } = 1;
    public int MaxLevel { get; private set; } = 3;

    public int [] LevelPrices { get; private set; } = { 100, 250, 625 };
    public int [] DurabilityModifiers { get; private set; } = { 100, 200, 300 };


    public List<SlotPosition> slots { get; private set; } = new List<SlotPosition> { };

    public int MaxModules { get; private set; } = 4;

    public string ModuleId { get; private set; }
    public Hull()
    {
        ModuleId = GenerateUniqueId();

        for (int i = 0; i < 4; i++)
        {
            SlotPosition slot = new SlotPosition(i);
            slots.Add(slot);
        }
    }

    public string GenerateUniqueId()
    {
        return $"{ModuleName}_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(0, 10000)}";
    }
    

}

