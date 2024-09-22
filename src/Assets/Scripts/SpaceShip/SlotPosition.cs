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

            return false; // ���������� false, ���� ���� ��� �����
        }

        occupiedModule = module;
        return true; // ������� ���������� ������
    }

    // ����������� ������, ���� �� ����� ������ �������
    public bool DetachModule(IModule module)
    {
        if (occupiedModule != module)
        {

            return false; // ���������� false, ���� �������� ����������� ������ ������
        }

        occupiedModule = null;
        return true; // ������� �����������
    }
}
