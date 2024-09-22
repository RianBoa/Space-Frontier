using UnityEngine;

public class PirateShip
{
    public int Durability { get; private set; }
    public int Damage { get; private set; }

    public PirateShip(int baseDurability, int baseDamage, int level)
    {
        Durability = baseDurability + level * 10; // ��������� ������������� � ������ �������
        Damage = baseDamage + level * 2; // ���� ������������� � ������ �������
    }

    public void TakeDamage(int amount)
    {
        Durability -= amount;
        if (Durability < 0)
        {
            Durability = 0;
        }
    }
}