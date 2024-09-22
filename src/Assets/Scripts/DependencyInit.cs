using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencyInit
{

        // ������������� ModuleHandleController
        public ModuleHandleController InitializeModuleHandleController(
            ShipModuleAggregator aggregator,
            ShipModuleStats stats,
            ShipModuleContainer container,
            ModuleShop shop,
            ModulePlacementManager placementManager)
        {
            return new ModuleHandleController(aggregator, stats, container, shop, placementManager);
        }

        // ������������� ResourceManager � ���������� ���������� ��������
        public ResourceManager InitializeResourceManager(int initialOre = 0, int initialEnergy = 50000, int initialCrypto = 2500)
        {
            return new ResourceManager(initialOre, initialEnergy, initialCrypto);
        }

        // ������������� SlotMovementManager
        public SlotMovementManager InitializeSlotMovementManager()
        {
            return new SlotMovementManager();
        }

        // ������������� ShipModuleAggregator, ������� ���������� �������������� �������
        public ShipModuleAggregator InitializeModuleAggregator(ShipModuleContainer container, ShipModuleStats stats)
        {
            return new ShipModuleAggregator(container, stats);
        }

        // ������������� ModuleShop, ������� ��������� �������� � �������� �������
        public ModuleShop InitializeModuleShop(ResourceManager resourceManager, ModulePlacementManager placementManager, ModuleFactory factory)
        {
            return new ModuleShop(resourceManager, placementManager, factory);
        }

        // ������������� ShipModuleStats ��� �������� ������������� �������
        public ShipModuleStats InitializeShipModuleStats()
        {
            return new ShipModuleStats();
        }

        // ������������� ModuleFactory, ������� �������� �� �������� �������
        public ModuleFactory InitializeModuleFactory()
        {
            return new ModuleFactory();
        }

        // ������������� ShipModuleContainer ��� �������� ���� ������� �������
        public ShipModuleContainer InitializeShipModuleContainer()
        {
            return new ShipModuleContainer();
        }

        // ������������� ModulePlacementManager, ������� ��������� ����������� ������� �� �������
        public ModulePlacementManager InitializeModulePlacementManager()
        {
            return new ModulePlacementManager();
    }


    // ������������� BattleManager, ������� ��������� ����������
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
