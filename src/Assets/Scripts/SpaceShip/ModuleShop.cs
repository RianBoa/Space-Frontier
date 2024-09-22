using System;



public class ModuleShop
{
    ResourceManager resourceManager;
    ModulePlacementManager placementManager;
    ModuleFactory factory;

    public ModuleShop(ResourceManager resourceManager, ModulePlacementManager placementManager, ModuleFactory factory)
    {
        this.resourceManager = resourceManager;
        this.placementManager = placementManager;
        this.factory = factory;
    }

    public bool TryPurchaseCommandCenter(out CommandCenter com)
    {
        com = null;
            com = (CommandCenter)factory.CreateModule(ModuleType.CommandCenter);

        if (!resourceManager.SpendResource(ResourceType.Crypto, com.LevelPrices[com.Level - 1]))
        {
            com = default;
            return false;
        }

        return true;
    }


    public bool TryPurchaseHull(out Hull hull, CommandCenter com)
    {
        hull = null;

       hull = factory.CreateModule(ModuleType.Hull) as Hull;
     
        if (!resourceManager.SpendResource(ResourceType.Crypto, hull.LevelPrices[0]))
        {

            hull = default;
            return false;
        }

        return true;
    }

    // Універсальний метод для покупки модулів
    public bool TryPurchaseModule(int slotIndex, Hull hullTarget, ModuleType moduleType, out IModule module)
    {
        module = default;
       
        module = factory.CreateModule(moduleType);
     

        if (!resourceManager.SpendResource(ResourceType.Crypto, module.LevelPrices[0]))
        {
            module = default;
            return false;
        }

        return true;
    }

    public bool TryUpgradeModule(IModule module)
    {

        if (module.Level >= module.MaxLevel)
            return false;

        if (!resourceManager.SpendResource(ResourceType.Crypto, module.LevelPrices[module.Level]))
            return false;

        module.Level++;
        return true;
    }

    public void SellModule(IModule module)
    {
        int sellPrice = module.LevelPrices[module.Level - 1] / 2;
        resourceManager.AddResource(ResourceType.Crypto, sellPrice);
    }
    public bool SellHull(Hull hull)
    {
        int sellPrice = hull.LevelPrices[hull.Level - 1] / 2; // Определяем цену продажи корпуса
        resourceManager.AddResource(ResourceType.Crypto, sellPrice); // Добавляем средства за корпус
        return true;
    }
}