using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyInit
{

        // Инициализация ModuleHandleController
        public ModuleHandleController InitializeModuleHandleController(
            ShipModuleAggregator aggregator,
            ShipModuleStats stats,
            ShipModuleContainer container,
            ModuleShop shop,
            ModulePlacementManager placementManager)
        {
            return new ModuleHandleController(aggregator, stats, container, shop, placementManager);
        }

        // Инициализация ResourceManager с начальными значениями ресурсов
        public ResourceManager InitializeResourceManager(int initialOre = 0, int initialEnergy = 50000, int initialCrypto = 2500)
        {
            return new ResourceManager(initialOre, initialEnergy, initialCrypto);
        }

        // Инициализация SlotMovementManager
        public SlotMovementManager InitializeSlotMovementManager()
        {
            return new SlotMovementManager();
        }

        // Инициализация ShipModuleAggregator, который агрегирует характеристики модулей
        public ShipModuleAggregator InitializeModuleAggregator(ShipModuleContainer container, ShipModuleStats stats)
        {
            return new ShipModuleAggregator(container, stats);
        }

        // Инициализация ModuleShop, который управляет покупкой и продажей модулей
        public ModuleShop InitializeModuleShop(ResourceManager resourceManager, ModulePlacementManager placementManager, ModuleFactory factory)
        {
            return new ModuleShop(resourceManager, placementManager, factory);
        }

        // Инициализация ShipModuleStats для хранения характеристик корабля
        public ShipModuleStats InitializeShipModuleStats()
        {
            return new ShipModuleStats();
        }

        // Инициализация ModuleFactory, который отвечает за создание модулей
        public ModuleFactory InitializeModuleFactory()
        {
            return new ModuleFactory();
        }

        // Инициализация ShipModuleContainer для хранения всех модулей корабля
        public ShipModuleContainer InitializeShipModuleContainer()
        {
            return new ShipModuleContainer();
        }

        // Инициализация ModulePlacementManager, который управляет размещением модулей на корабле
        public ModulePlacementManager InitializeModulePlacementManager()
        {
            return new ModulePlacementManager();
    }


    // Инициализация BattleManager, который управляет сражениями
    public BattleManager InitializeBattleManager()
    {
        return new BattleManager();
    }
    public GameProgress InitializeGameProgress(IResourceManager resourceManager, ShipModuleStats shipModuleStats, BattleManager battle, ModuleHandleController m_handle)
    {
        return new GameProgress(resourceManager, shipModuleStats, battle, m_handle);
    }
    public ShipFunctionsModel InitializeShipFunctionsModule(ShipModuleStats shipModuleStats, IResourceManager resourceManager)
    {
        return new ShipFunctionsModel(shipModuleStats, resourceManager);
    }
}
