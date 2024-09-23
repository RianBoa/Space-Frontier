using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ModulePlacementManager : MonoBehaviour
{

    public bool CanAddHull(Hull hull, CommandCenter commandCenter)
    {
        if (commandCenter == null)
        {
           
            return false;
        }

        int maxHulls = commandCenter.MaxHulls[commandCenter.Level - 1];
        int currentHulls = commandCenter.Hulls.Count;

      

        if (currentHulls >= maxHulls)
        {
          
            return false;
        }

        if (commandCenter.Hulls.Contains(hull))
        {
            return false;
        }

        return true;
    }
    public bool CanAddEngine(CommandCenter commandCenter, Hull hull)
    {
        if (commandCenter == null)
        {
      
            return false;
        }

        int engineCount = 0;

        foreach (var hulls in commandCenter.Hulls)
        {
            engineCount += GetEngineCount(hulls);
        }

        return engineCount < commandCenter.Hulls.Count / 2;
    }

    public bool CanAttachModuleToHull(ModuleType moduleType, Hull targetHull, int slotIndex, CommandCenter commandCenter)
    {

        if (moduleType == ModuleType.Hull)
            return false;


        if (moduleType == ModuleType.Engine && !CanAddEngine(commandCenter, targetHull))
        {
            
            return false;
        }



        return true;
    }
    public bool RemoveModuleFromHull(IModule module, Hull hullTarget, int slotIndex)
    {
        if (hullTarget == null)
            return false;

        var slot = hullTarget.slots[slotIndex];
        if (slot.occupiedModule != module)
            return false;

        slot.DetachModule(module);

        return true;
    }

    public int GetEngineCount(Hull hull)
    {
        return hull.slots.Count(s => s.occupiedModule is Engine);
    }
    public SlotPosition CheckSlotForModule(ModuleType moduleType, int slotIndex, Hull hull)
    {
       
        var slot = hull.slots.FirstOrDefault(s => s.SlotIndex == slotIndex);
        if (slot == null)
        {
           
            return null;
        }

        if (slot.IsOccupied)
        {
      
            return null;
        }

        var leftSlot = hull.slots.FirstOrDefault(s => s.SlotIndex == slotIndex - 1);
        var rightSlot = hull.slots.FirstOrDefault(s => s.SlotIndex == slotIndex + 1);

        // Проверяем несовместимость с модулем слева
        if (leftSlot != null && leftSlot.IsOccupied && !ModuleInfo.IsCompatible(moduleType, leftSlot.occupiedModule.ModuleType))
        {
          
            return null;
        }

        // Проверяем несовместимость с модулем справа
        if (rightSlot != null && rightSlot.IsOccupied && !ModuleInfo.IsCompatible(moduleType, rightSlot.occupiedModule.ModuleType))
        {
         
            return null;
        }

        // Если проверка пройдена, создаём и устанавливаем модуль


        return slot;
    }
    public void AttachModuleToSlot(IModule module, SlotPosition slot)
    {
         slot.AttachModule(module);
    }
}