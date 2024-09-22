using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Ore,
    Energy,
    Crypto
}

public class ResourceManager : IResourceManager
{  private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();
    public void AddResource(ResourceType resourceType, int amount)
    {
        resources[resourceType] += amount;
    }
    public bool SpendResource(ResourceType resourceType, int amount)
    {
        if (resources[resourceType] >= amount)
        {
            resources[resourceType] -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public int GetResourceAmount(ResourceType resourceType)
    {
        if(resources.ContainsKey(resourceType))
           return resources[resourceType];
        return 0;
    }

    public ResourceManager(int initialOre, int initialEnergy, int initialCrypo)
     {
        resources = new Dictionary<ResourceType, int>
       { 
         { ResourceType.Ore, initialOre },
         { ResourceType.Energy, initialEnergy },
         { ResourceType.Crypto, initialCrypo },
        };
     }
}
