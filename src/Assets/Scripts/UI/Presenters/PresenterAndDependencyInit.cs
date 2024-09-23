using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PresenterAndDependencyInit : MonoBehaviour
{
    [SerializeField] private CommandCenterView commandCenterView;
    [SerializeField] private HullPanelView hullPanelView;
    [SerializeField] private ModuleSelectionView moduleSelectionView;
    [SerializeField] private List<SlotButtonView> slotButtonViews;
    [SerializeField] private ResourceTradeView resourceTradeView;
    [SerializeField] private GameProgressView gameProgressView;
    [SerializeField] private ShipFunctionsView shipFunctionsView;
    [SerializeField] private ResourceUI resourceUI;
    [SerializeField] private ShipStatsView shipStatsView;
    [SerializeField] private PlanetSpawner planetSpawner;

    private void Start()
    {
        // Инициализация зависимостей
        DependencyInit dependencyInit = new DependencyInit();

        var resourceManager = dependencyInit.InitializeResourceManager();
        var moduleFactory = dependencyInit.InitializeModuleFactory();
        var slotMovementManager = dependencyInit.InitializeSlotMovementManager();
        var shipModuleStats = dependencyInit.InitializeShipModuleStats();
        var shipModuleContainer = dependencyInit.InitializeShipModuleContainer();
        var modulePlacementManager = dependencyInit.InitializeModulePlacementManager();
        var moduleAggregator = dependencyInit.InitializeModuleAggregator(shipModuleContainer, shipModuleStats);
        var moduleShop = dependencyInit.InitializeModuleShop(resourceManager, modulePlacementManager, moduleFactory);
        var battleManager = dependencyInit.InitializeBattleManager();
        var shipModule = dependencyInit.InitializeShipFunctionsModule(shipModuleStats, resourceManager);
       
        shipStatsView.SetStats(shipModuleStats, shipModuleContainer);
        // Инициализация ModuleHandleController
        var moduleHandleController = dependencyInit.InitializeModuleHandleController(
            moduleAggregator, shipModuleStats, shipModuleContainer, moduleShop, modulePlacementManager      
        );
        var gameProgress = dependencyInit.InitializeGameProgress(resourceManager, shipModuleStats, battleManager, moduleHandleController);
        resourceUI.SetResourceManager(resourceManager);
        planetSpawner.OnAsteroidsInit += gameProgressView.SetAsteroid;

       // Создание фабрики презентеров
       PresenterFactory presenterFactory = new PresenterFactory(
            moduleHandleController,
            slotMovementManager,
            resourceManager
        );

        // Инициализация презентеров
        PresenterInitializer presenterInitializer = new PresenterInitializer(presenterFactory);
        presenterInitializer.InitializePresenters(
          
        commandCenterView ,
        hullPanelView, 
        moduleSelectionView,
        slotButtonViews.Cast<ISlotButtonView>().ToList(), // Приведение каждого элемента к интерфейсу
        resourceTradeView ,
        gameProgressView, 
        shipFunctionsView,
            moduleHandleController,
            slotMovementManager,
            resourceManager,
            moduleFactory,
            shipModule,
            gameProgress
        );

 
    }
  
}
