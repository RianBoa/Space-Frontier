
using System.Collections.Generic;


public class ShipModuleContainer
{
    private Dictionary<string, IModule> modules = new Dictionary<string, IModule>();


    public void AddModule(IModule module)
    {
        if (!modules.ContainsKey(module.ModuleId))
        {
            modules[module.ModuleId] = module;
        }
    }

  
    public IModule GetModuleById(string moduleId)
    {
        modules.TryGetValue(moduleId, out var module);
        return module;
    }


    public List<T> GetModulesByType<T>() where T : IModule
    {
        List<T> result = new List<T>();

        foreach (var module in modules.Values)
        {
            if (module is T)
            {
                result.Add((T)module);
            }
        }

        return result;
    }


    public bool RemoveModule(string moduleId)
    {
        return modules.Remove(moduleId);
    }

    public List<string> GetAllModulesById()
    {
        return new List<string>();
    }
}