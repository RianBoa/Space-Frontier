using System.IO;
using UnityEngine;
using System.Collections;

public class BattleManager
{
    private ShipModuleStats playerStats;
    private PirateShip pirate;
    private string logFilePath = "battle_log.txt"; // ���� � ����� ����

    public delegate void BattleResultHandler(bool playerWon);
    public event BattleResultHandler OnBattleEnded;

    public IEnumerator StartBattle(IResourceManager manager, PirateShip ship)
    {
       return BattleRoutine(manager, ship);
    }
    private IEnumerator BattleRoutine(IResourceManager resource, PirateShip pirateShip)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath))
        {
            writer.WriteLine("Battle started with a pirate ship!");
            while (pirate.Durability > 0 && playerStats.CurrentDurability > 0)
            {
                // ����� ������� �������
                pirate.TakeDamage(playerStats.CurrentDamage);
                writer.WriteLine($"Player attacked pirate, dealing {playerStats.CurrentDamage} damage. Pirate durability: {pirate.Durability}");
               
               
                    // ����� ������� ������� � ������ �������
                    pirate.TakeDamage(playerStats.CurrentDamage);
                resource.SpendResource(ResourceType.Energy, playerStats.CurrentEnergyComsumptionPerShot);
                    writer.WriteLine($"Player attacked pirate, dealing {playerStats.CurrentDamage} damage. Pirate durability: {pirate.Durability}");
                    writer.WriteLine($"Player consumed {playerStats.CurrentEnergyComsumptionPerShot} energy.");
                    if (pirate.Durability <= 0)
                {
                    writer.WriteLine("Pirate ship destroyed! Player wins!");
                    OnBattleEnded?.Invoke(true); // ��������, ��� ����� �������
                    yield break;
                }

                // ������ ������� ������
                playerStats.CurrentDurability -= pirate.Damage;
                writer.WriteLine($"Pirate attacked player, dealing {pirate.Damage} damage. Player durability: {playerStats.CurrentDurability}");

                if (playerStats.CurrentDurability <= 0)
                {
                    writer.WriteLine("Player's ship destroyed! Game Over.");
                    OnBattleEnded?.Invoke(false); // ��������, ��� ����� ��������
                    yield break;
                }

                yield return new WaitForSeconds(1f); // ���� ���� ������� ����� �������
            }
        }
    }

    public void SetShipStats(ShipModuleStats playerStats)
    {
        this.playerStats = playerStats;
    }
}
