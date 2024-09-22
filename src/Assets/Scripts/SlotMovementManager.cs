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
            Debug.LogError("—лот, из которого пытаемс€ переместить модуль, пуст!");
            return;
        }

        var module = fromSlot.occupiedModule;

        // ѕробуем прикрепить модуль к целевому слоту
        if (toSlot.AttachModule(module))
        {
            // ≈сли успешно переместили модуль, отсоедин€ем его от старого слота
            if (fromSlot.DetachModule(module))
            {
                Debug.Log($"ћодуль {module.ModuleName} перемещЄн из слота {fromIndex} в слот {toIndex}");
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
