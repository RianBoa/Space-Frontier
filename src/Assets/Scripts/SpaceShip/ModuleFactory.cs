using System;
using System.Collections.Generic;


public enum ModuleType
{
    Engine,
    Cannon,
    Converter,
    Hull,
    CommandCenter,
    Repairer,
    Storage,
    Generator,
    Battery,
    Collector
}

public class ModuleFactory
{
    private Dictionary<ModuleType, Func<IModule>> moduleFactory;

    public ModuleFactory()
    {
        moduleFactory = new Dictionary<ModuleType, Func<IModule>>
        {
            
            { ModuleType.Engine, () => new Engine() },
            { ModuleType.Cannon, () => new Cannon() },
            { ModuleType.Battery, () => new Battery() },
            { ModuleType.Storage    , () => new Storage() },
            { ModuleType.Converter, () => new Converter() },
            { ModuleType.Collector, () => new Collector() },
            { ModuleType.Generator, () => new Generator() },
            { ModuleType.Repairer   , () => new Repairer() },
            { ModuleType.CommandCenter, () => new CommandCenter() },
               { ModuleType.Hull, () => new Hull() },
        };
    }

 public IModule CreateModule(ModuleType moduleType)
    {
        if (!moduleFactory.TryGetValue(moduleType, out var module))
        {
            throw new ArgumentException($"Unknown module type: {moduleType}");
        }
        return module();
    }
    public IEnumerable<ModuleType> GetAllModuleKeys()
    {
        return moduleFactory.Keys;
    }    
}
