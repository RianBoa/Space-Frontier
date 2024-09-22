using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotMovementManager 
{
    public event Action<Hull> ModuleMoved;
    public void MoveModule(Hull hull, int fromIndex, int toIndex)
    {
        var fromSlot = hull.slots[fromIndex];
        var toSlot = hull.slots[toIndex];

        if (!fromSlot.IsOccupied)
        {
            Debug.LogError("����, �� �������� �������� ����������� ������, ����!");
            return;
        }

        var module = fromSlot.occupiedModule;

        // ������� ���������� ������ � �������� �����
        if (toSlot.AttachModule(module))
        {
            // ���� ������� ����������� ������, ����������� ��� �� ������� �����
            if (fromSlot.DetachModule(module))
            {
                Debug.Log($"������ {module.ModuleName} ��������� �� ����� {fromIndex} � ���� {toIndex}");
                ModuleMoved?.Invoke(hull);
            }
        }
      
    }
    public bool CanMoveLeft(Hull hull, int index)
    {
        return index > 0 && !hull.slots[index - 1].IsOccupied;
    }
    
    public bool CanMoveRight(Hull hull, int index)
    {
        return index < hull.slots.Count - 1  && !hull.slots[index + 1].IsOccupied;
    }

}
