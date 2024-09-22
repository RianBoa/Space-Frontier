using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ShipModuleAggregator
{
   ShipModuleContainer moduleContainer;
   ShipModuleStats shipStats;

    public ShipModuleAggregator(ShipModuleContainer moduleContainer, ShipModuleStats shipStats)
    {
        this.moduleContainer = moduleContainer;
        this.shipStats = shipStats;
    }

    public void AggregateStats()
    {
        // Инициализация переменные для суммирования характеристик
        int totalDurabilityModifier = 0;
        int totalEnergyConsumptionPerBattle = 0;
        int totalEnergyConsumptionPerDistance = 0;
        int totalEnergyGenerationPerDistance = 0;
        int totalEnergyConversionRate = 1; // Начальное значение для мультипликации
        int totalEnergyConsumptionPerCollection = 0;
        int totalEnergyConsumptionPerRepair = 0;
        int totalEnergyConsumptionPerShot = 0;
        int totalRepairRate = 0;
        int totalOreStorageCapacity = 0;
        int totalEnergyCapacity = 0;
        int totalOreCollectionRate = 0;
        int totalDamage = 0;
        int totalSpeedIncrease = 0;
        int totalAcceleration = 0;
        int totalMaxHull = 0;

        foreach (var moduleId in moduleContainer.GetAllModulesById())
        {
            var module = moduleContainer.GetModuleById(moduleId);
            // Общие характеристики
            totalDurabilityModifier += module.DurabilityModifiers[module.Level - 1];

            switch (module)
            {
                case Cannon cannon:
                    totalDamage += cannon.LevelDamageMultiplier[cannon.Level - 1];
                    totalEnergyConsumptionPerShot += cannon.EnergyComsumptionPerShot;
                    break;

                case Engine engine:
                    totalSpeedIncrease += engine.SpeedModifier[engine.Level - 1];
                    totalEnergyConsumptionPerBattle += engine.EnergyConsumptionPerBattle[engine.Level - 1];
                    totalEnergyConsumptionPerDistance += engine.EnergyConsumptionPerDistance[engine.Level - 1];
                    totalAcceleration += engine.AccelerationModifiers[engine.Level - 1];
                    break;

                case Battery battery:
                    totalEnergyCapacity += battery.EnergyCapacityModifier[battery.Level - 1];
                    break;

                case Repairer repairer:
                    totalRepairRate += repairer.RepairRate[repairer.Level - 1];
                    totalEnergyConsumptionPerRepair += repairer.EnergyConsumptionPerRepair;
                    break;

                case Storage storage:
                    totalOreStorageCapacity += storage.OreStorageCapacity[storage.Level - 1];
                    break;

                case Collector collector:
                    totalOreCollectionRate += collector.CollectRateModifier[collector.Level - 1];
                    totalEnergyConsumptionPerCollection += collector.EnergyConsumptionPerCollect;
                    break;

                case Generator generator:
                    totalEnergyGenerationPerDistance += generator.EnergyGenerationModifier[generator.Level - 1];
                    break;

                case Converter converter:
                    totalEnergyConversionRate *= converter.OreConverterRate[converter.Level - 1];
                    break;

                case CommandCenter commandCenter:
                    totalMaxHull += commandCenter.MaxHulls[commandCenter.Level - 1];
                    break;

                case Hull hull:
                    break;
            }
        }

        // Обновляем характеристики корабля в ShipModuleStats
        shipStats.CurrentDurability = shipStats.BaseDurability + totalDurabilityModifier;
        shipStats.CurrentSpeed = totalSpeedIncrease;
        shipStats.CurrentEnergyConsumptionPerBattle = totalEnergyConsumptionPerBattle;
        shipStats.CurrentEnergyConsumptionPerDistance = totalEnergyConsumptionPerDistance;
        shipStats.CurrentEnergyGenerationPerDistance = totalEnergyGenerationPerDistance;
        shipStats.CurrentEnergyConversionRate = totalEnergyConversionRate;
        shipStats.CurrentEnergyConsumptionPerCollection = totalEnergyConsumptionPerCollection;
        shipStats.CurrentEnergyConsumptionPerRepair = totalEnergyConsumptionPerRepair;
        shipStats.CurrentEnergyComsumptionPerShot = totalEnergyConsumptionPerShot;
        shipStats.CurrentRepairRate = totalRepairRate;
        shipStats.CurrentOreStorageCapacity = totalOreStorageCapacity;
        shipStats.CurrentEnergyCapacity = totalEnergyCapacity;
        shipStats.CurrentOreCollectionRate = totalOreCollectionRate;
        shipStats.CurrentDamage = totalDamage;
        shipStats.CurrentMaxHull = totalMaxHull;
        shipStats.CurrentAcceleration = totalAcceleration;
    }
}
