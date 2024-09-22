using System.Collections.Generic;

public class PresenterFactory : IPresenterFactory
{
    private readonly IModuleHandler moduleHandler;
    private readonly SlotMovementManager slotMovementManager;
    private readonly IResourceManager resourceManager;

    public PresenterFactory(IModuleHandler moduleHandler, SlotMovementManager slotMovementManager, IResourceManager resourceManager)
    {
        this.moduleHandler = moduleHandler;
        this.slotMovementManager = slotMovementManager;
        this.resourceManager = resourceManager;
    }

    public CommandCenterPresenter CreateCommandCenterPresenter(ICommandCenterView commandCenterView, IModuleHandler moduleHandler, HullPanelPresenter hullPanelPresenter)
    {
        return new CommandCenterPresenter(commandCenterView, moduleHandler, hullPanelPresenter);
    }
    public HullPanelPresenter CreateHullPanelPresenter(IHullPanelView hullPanelView, SlotButtonPresenter[] slots, ModuleSelectionPresenter moduleSelectionPresenter)
    {
        return new HullPanelPresenter(hullPanelView,slots, moduleSelectionPresenter);
    }

    public ModuleSelectionPresenter CreateModuleSelectionPresenter(IModuleSelectionView moduleView, IModuleHandler moduleHandler, ModuleFactory moduleFactory)
    {
        return new ModuleSelectionPresenter(moduleView, moduleFactory, moduleHandler);
    }
    public SlotButtonPresenter CreateSlotButtonPresenter(ISlotButtonView slotView, IModuleHandler moduleHandler, SlotMovementManager slotMovementManager, ModuleSelectionPresenter moduleSelectionPresenter)
    {
        return new SlotButtonPresenter(slotView, moduleHandler,slotMovementManager, moduleSelectionPresenter); 
    }
    public ResourceTradePresenter CreateResourceTradePresenter(IResourceTradeView resourceTradeView, IResourceManager resourceManager)
    {
        return new ResourceTradePresenter(resourceTradeView, resourceManager);
    }
    public GameProgressPresenter CreateGameProgressPresenter(IGameProgressView gameProgressView, IGameProgress gameProgressModel)
    {
        return new GameProgressPresenter(gameProgressView, gameProgressModel);
    }
    public ShipFunctionsPresenter CreateShipFunctionsPresenter(IShipFunctionsView s_view, IShipFunctionsModel shipFunctionsModel, IGameProgress gameProgress)
    {
        return new ShipFunctionsPresenter(s_view, shipFunctionsModel, gameProgress);
    }

}
