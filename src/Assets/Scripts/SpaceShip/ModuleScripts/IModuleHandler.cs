
using System;

public interface IModuleHandler
{
    public bool HandleModuleSelection(ModuleType moduleType, Hull hullTarget, int slotIndex);
    public bool HandleCenterPurchase();
    public bool HandleHullPurchase();
    public bool HandleModuleUpgrade(IModule module);
    public bool HandleModuleSold(IModule module, Hull hull, int index);
    public bool HandleHullSold(Hull hull);

    public CommandCenter GetCommandCenter();
    public event Action<CommandCenter> CommandCenterPurchased;
    public event Action<CommandCenter> HullPurchased;
    public bool TryPurchaseAllRequiresModules();
}
