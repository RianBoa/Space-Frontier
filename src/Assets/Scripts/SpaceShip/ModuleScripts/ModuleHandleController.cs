using UnityEngine;
using System;
using System.Collections.Generic;

public class ModuleHandleController : IModuleHandler
{
    private ShipModuleAggregator shipModuleAggregator;
    private ShipModuleStats shipModuleStats;
    private ShipModuleContainer shipModuleContainer;
    private ModuleShop moduleShop;
    private ModulePlacementManager modulePlacementManager;
   
    private CommandCenter commandCenter;

    private HashSet<ModuleType> requiredModules = new HashSet<ModuleType>
    {
        ModuleType.CommandCenter, ModuleType.Hull, ModuleType.Cannon, ModuleType.Generator, ModuleType.Engine, ModuleType.Battery, ModuleType.Collector, ModuleType.Converter, ModuleType.Repairer, ModuleType.Storage
    };
    private HashSet<ModuleType> purchasedModules = new HashSet<ModuleType>();

    public event Action AllModulesPurchased;
    public event Action<CommandCenter> CommandCenterPurchased;
    public event Action<CommandCenter> HullPurchased;


    public ModuleHandleController(ShipModuleAggregator shipModuleAggregator, ShipModuleStats shipModuleStats, ShipModuleContainer shipModuleContainer, ModuleShop moduleShop, ModulePlacementManager modulePlacementManager)
    {   this.shipModuleAggregator = shipModuleAggregator;
        this.shipModuleStats = shipModuleStats;
        this.shipModuleContainer = shipModuleContainer;
        this.moduleShop = moduleShop;
        this.modulePlacementManager = modulePlacementManager;
    }


    public bool HandleCenterPurchase()
    {
        if (!moduleShop.TryPurchaseCommandCenter(out commandCenter))
        {
            return false;
        }
        AttachCommandCenter(commandCenter);
        shipModuleAggregator.AggregateStats();
        CheckAllRequiredModules(ModuleType.CommandCenter);
        CommandCenterPurchased?.Invoke(commandCenter);
        return true;
    }

    public bool HandleModuleSelection(ModuleType moduleType, Hull hullTarget, int slotIndex)
    {
        // ���� 1: �������� ��������� ������������ ������ (���������, �������� �������)
        if (!modulePlacementManager.CanAttachModuleToHull(moduleType, hullTarget, slotIndex, commandCenter))
        {
            return false;
        }

        // ���� 2: �������� ����� ����� ��������
        SlotPosition slot = modulePlacementManager.CheckSlotForModule(moduleType, slotIndex, hullTarget);
        if (slot == null)
        {
            return false;
        }

        // ���� 3: ������� ������
        IModule purchasedModule;
        if (!moduleShop.TryPurchaseModule(slotIndex, hullTarget, moduleType, out purchasedModule))
        {
           return false;
        }
        // ���� 4: ��������� ������ �� �����
        modulePlacementManager.AttachModuleToSlot(purchasedModule, slot);

        // ��������� �������������
        CheckAllRequiredModules(moduleType);
        shipModuleContainer.AddModule(purchasedModule);
        shipModuleAggregator.AggregateStats();
        return true;
    }

    private void AttachCommandCenter(CommandCenter commandCenter)
    {
        this.commandCenter = commandCenter;
        shipModuleContainer.AddModule(commandCenter);
    }



    public bool HandleHullPurchase()
    {
        Hull newHull = null;
        if (modulePlacementManager.CanAddHull(newHull, commandCenter))
        {
            if (moduleShop.TryPurchaseHull(out newHull, commandCenter))
            {
                shipModuleContainer.AddModule(newHull);
                shipModuleAggregator.AggregateStats();
                commandCenter.Hulls.Add(newHull);

                CheckAllRequiredModules(ModuleType.Hull);
                HullPurchased?.Invoke(commandCenter);
                return true;
            }
            else { return false; }
        }
         else return false;
    }

        public bool HandleModuleUpgrade(IModule module)
        {
        if (shipModuleContainer.GetModuleById(module.ModuleId) != null)
          {
            moduleShop.TryUpgradeModule(module);
            shipModuleAggregator.AggregateStats();
            return true;
          }
         else
        {
            return false;
        }

    }

    public bool HandleModuleSold(IModule module, Hull hull, int index)
    {
        if (modulePlacementManager.RemoveModuleFromHull(module, hull, index))
        {
            moduleShop.SellModule(module);
            shipModuleContainer.RemoveModule(module.ModuleId);
            shipModuleAggregator.AggregateStats();

            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HandleHullSold(Hull hull)
    {
        // ������� ��� ������ � �������
        for (int i = 0; i < hull.slots.Count; i++)
        {
            if (hull.slots[i].IsOccupied)
            {
                var module = hull.slots[i].occupiedModule;
                if (!HandleModuleSold(module, hull, i))
                {
                    return false; // ��������� �������, ���� ������� ������-���� ������ �� �������
                }
            }
        }
        if (!moduleShop.SellHull(hull))
        {
            return false;
        }

        
        commandCenter.Hulls.Remove(hull);

        return true;
    }
    public void CheckAllRequiredModules(ModuleType module)
    {
        if (requiredModules.Contains(module))
        {
            if (!purchasedModules.Contains(module))
            {
                purchasedModules.Add(module);
            }
            if (purchasedModules.Count == requiredModules.Count)
            {
                AllModulesPurchased?.Invoke();
            }
        }

    }
    public CommandCenter GetCommandCenter()
    {
        if(commandCenter != null)
            return commandCenter;
        else 
            return null;
    }
   public bool TryPurchaseAllRequiresModules()
    {
        if(HandleCenterPurchase())
        {
            if (HandleHullPurchase())
            {
                Hull newHull = commandCenter.Hulls[commandCenter.Hulls.Count - 1];
                HandleModuleSelection(ModuleType.Storage, newHull, 0);
                HandleModuleSelection(ModuleType.Repairer, newHull, 1);
                HandleModuleSelection(ModuleType.Generator, newHull, 2);
                HandleModuleSelection(ModuleType.Collector, newHull, 3);
            }
            else return false;
            if (HandleHullPurchase())
            {
                Hull newHull = commandCenter.Hulls[commandCenter.Hulls.Count - 1];
                HandleModuleSelection(ModuleType.Engine, newHull, 0);
                HandleModuleSelection(ModuleType.Battery, newHull, 1);
                HandleModuleSelection(ModuleType.Cannon, newHull, 2);
            }
            if(HandleHullPurchase())
            {
                Hull newHull = commandCenter.Hulls[commandCenter.Hulls.Count - 1];
                HandleModuleSelection(ModuleType.Converter, newHull, 0);
            }
            else return false;

        }
     
        return true;
    } 
}
