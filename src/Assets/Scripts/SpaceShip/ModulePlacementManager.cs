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
            Debug.LogError("��������� ����� �� ����������.");
            return false;
        }

        int maxHulls = commandCenter.MaxHulls[commandCenter.Level - 1];
        int currentHulls = commandCenter.Hulls.Count;

        Debug.Log($"�������� ���������� �������: ������� ������� {currentHulls}, �������� ��� ������ {maxHulls}");

        if (currentHulls >= maxHulls)
        {
            Debug.LogWarning("��������� ������������ ����� �������� ��� �������� ������ ���������� ������.");
            return false;
        }

        if (commandCenter.Hulls.Contains(hull))
        {
            Debug.LogWarning("���� ������ ��� �������� � ��������� �����.");
            return false;
        }

        return true;
    }
    public bool CanAddEngine(CommandCenter commandCenter, Hull hull)
    {
        if (commandCenter == null)
        {
            Debug.LogError("CommandCenter �� ����������� � ModulePlacementManager.");
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
            Debug.Log("������ �������");
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
        Debug.Log("������ " + hull.ModuleId);
        Debug.Log($"��������� ���� � ��������: {slotIndex}");
        var slot = hull.slots.FirstOrDefault(s => s.SlotIndex == slotIndex);
        if (slot == null)
        {
            Debug.Log("�� ���� ������ �����");
            return null;
        }

        if (slot.IsOccupied)
        {
            Debug.Log("���� ��������");
            return null;
        }

        var leftSlot = hull.slots.FirstOrDefault(s => s.SlotIndex == slotIndex - 1);
        var rightSlot = hull.slots.FirstOrDefault(s => s.SlotIndex == slotIndex + 1);

        // ��������� ��������������� � ������� �����
        if (leftSlot != null && leftSlot.IsOccupied && !ModuleInfo.IsCompatible(moduleType, leftSlot.occupiedModule.ModuleType))
        {
            Debug.Log($"������ {ModuleInfo.GetModuleName(moduleType)} ����������� � {leftSlot.occupiedModule.ModuleName}.");
            return null;
        }

        // ��������� ��������������� � ������� ������
        if (rightSlot != null && rightSlot.IsOccupied && !ModuleInfo.IsCompatible(moduleType, rightSlot.occupiedModule.ModuleType))
        {
            Debug.Log($"������ {ModuleInfo.GetModuleName(moduleType)} ����������� � {rightSlot.occupiedModule.ModuleName}.");
            return null;
        }

        // ���� �������� ��������, ������ � ������������� ������


        return slot;
    }
    public void AttachModuleToSlot(IModule module, SlotPosition slot)
    {
         slot.AttachModule(module);
    }
}