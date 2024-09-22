using System.Collections;
using System.Collections.Generic;

public class SlotPosition
{
    public int SlotIndex { get; }

    public IModule occupiedModule { get; set; }

    public bool IsOccupied => occupiedModule != null;

    public SlotPosition(int slotIndex)
    {
        SlotIndex = slotIndex;
        occupiedModule = null;
    }

    public bool AttachModule(IModule module)
    {
        if (IsOccupied)
        {

            return false; // Возвращаем false, если слот уже занят
        }

        occupiedModule = module;
        return true; // Успешно прикрепили модуль
    }

    // Отсоединяет модуль, если он занят данным модулем
    public bool DetachModule(IModule module)
    {
        if (occupiedModule != module)
        {

            return false; // Возвращаем false, если пытаемся отсоединить другой модуль
        }

        occupiedModule = null;
        return true; // Успешно отсоединили
    }
}
