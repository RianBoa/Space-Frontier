using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ShipStatsView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI stats;

    private ShipModuleStats Stats;
    private ShipModuleContainer container;

 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stats.gameObject.SetActive(true);
            stats.text = $"Base Durability: {Stats.BaseDurability}\n" +
           $"Current Durability: {Stats.CurrentDurability}\n" +
           $"Current Speed: {Stats.CurrentSpeed}\n" +
           $"Energy Consumption Per Battle: {Stats.CurrentEnergyConsumptionPerBattle}\n" +
           $"Energy Consumption Per Distance: {Stats.CurrentEnergyConsumptionPerDistance}\n" +
           $"Energy Generation Per Distance: {Stats.CurrentEnergyGenerationPerDistance}\n" +
           $"Energy Conversion Rate: {Stats.CurrentEnergyConversionRate}\n" +
           $"Energy Consumption Per Collection: {Stats.CurrentEnergyConsumptionPerCollection}\n" +
           $"Energy Consumption Per Repair: {Stats.CurrentEnergyConsumptionPerRepair}\n" +
           $"Energy Consumption Per Shot: {Stats.CurrentEnergyComsumptionPerShot}\n" +
           $"Repair Rate: {Stats.CurrentRepairRate}\n" +
           $"Ore Storage Capacity: {Stats.CurrentOreStorageCapacity}\n" +
           $"Energy Capacity: {Stats.CurrentEnergyCapacity}\n" +
           $"Ore Collection Rate: {Stats.CurrentOreCollectionRate}\n" +
           $"Damage: {Stats.CurrentDamage}\n" +
           $"Max Hull: {Stats.CurrentMaxHull}\n" +
           $"Acceleration: {Stats.CurrentAcceleration}";
        }
       if(Input.GetKeyDown(KeyCode.R))
        {
            stats.gameObject.SetActive(true);
            ShowAllModules();
        }
       if(Input.GetKeyDown(KeyCode.T)) 
            {
            stats.gameObject.SetActive(false);
            }
    }

    public void SetStats(ShipModuleStats stats, ShipModuleContainer container)
    {

        Stats = stats;
        this.container = container;

    }
    private void ShowAllModules()
    {
        if (container == null) return;

        stats.gameObject.SetActive(true);
        stats.text = "Modules on ship:\n";

        // Получаем все модули из контейнера
        List<IModule> modules = container.GetAllModules();

        // Проходим по каждому модулю и добавляем информацию о нем
        foreach (IModule module in modules)
        {
            stats.text += $"Module ID: {module.ModuleId}, Type: {module.GetType().Name}, Level: {module.Level}\n";
        }
    }
}
