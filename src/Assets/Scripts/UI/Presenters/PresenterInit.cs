using System.Collections.Generic;
using Unity.VisualScripting;

public class PresenterInitializer
{
    private readonly PresenterFactory presenterFactory;
    private SlotButtonPresenter[] slots = new SlotButtonPresenter[4];
    public PresenterInitializer(PresenterFactory presenterFactory)
    {
        this.presenterFactory = presenterFactory;
    }

    public void InitializePresenters(
        ICommandCenterView commandCenterView,
        IHullPanelView hullPanelView,
        IModuleSelectionView moduleSelectionView,
        List<ISlotButtonView> slotButtonViews,
        IResourceTradeView resourceTradeView,
        IGameProgressView gameProgressView,
        IShipFunctionsView shipFuncView,
        IModuleHandler moduleHandler, // Добавляем IModuleHandler для презентеров
        SlotMovementManager slotMovementManager, // Передаём здесь для использования в презентерах
        IResourceManager resourceManager, // Передаём ResourceManager для торговли
        ModuleFactory moduleFactory,
        IShipFunctionsModel shipFunctionsModel,
        IGameProgress gameProgressModel)
    {

        // Инициализация ModuleSelectionPresenter
        var moduleSelectionPresenter = presenterFactory.CreateModuleSelectionPresenter(
            moduleSelectionView, moduleHandler,
            moduleFactory
        );

        for (int i = 0; i < slots.Length; i++)
    
            {
                var slotButtonPresenter = presenterFactory.CreateSlotButtonPresenter(slotButtonViews[i], moduleHandler, slotMovementManager, moduleSelectionPresenter);
                slots[i] = slotButtonPresenter;
            }


            // Инициализация HullPanelPresenter
            var hullPanelPresenter = presenterFactory.CreateHullPanelPresenter(
                hullPanelView,
                slots,

                moduleSelectionPresenter

            );

            // Инициализация CommandCenterPresenter
            var commandCenterPresenter = presenterFactory.CreateCommandCenterPresenter(
                commandCenterView,

                moduleHandler,
                hullPanelPresenter
            );

            // Инициализация ResourceTradePresenter
            var resourceTradePresenter = presenterFactory.CreateResourceTradePresenter(
                resourceTradeView, resourceManager
            );
            var gameProgressPresenter = presenterFactory.CreateGameProgressPresenter(gameProgressView, gameProgressModel);
            var shipFunctionsPresenter = presenterFactory.CreateShipFunctionsPresenter(shipFuncView, shipFunctionsModel, gameProgressModel);
        }
    }

