using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSourceIdCollection 
{
    public static Dictionary<string, IResourceSource> resourceId = new Dictionary<string, IResourceSource>();
    public static void AddResource(string id, IResourceSource resource)
    {
        if (!resourceId.ContainsKey(id))
        {
            resourceId.Add(id, resource);
        }
        else
        {
            Debug.LogWarning($"Resource with ID {id} already exists.");
        }
    }
    public static IResourceSource GetResourceById(string id)
    {
        if (resourceId.ContainsKey(id))
        {
            return resourceId[id];
        }
        else
        {
            Debug.LogWarning($"Resource with ID {id} not found.");
            return null;
        }
    }
}
