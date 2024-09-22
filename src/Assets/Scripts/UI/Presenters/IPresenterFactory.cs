using System.Collections.Generic;

public interface IPresenterFactory 
{
    HullPanelPresenter CreateHullPanelPresenter(IHullPanelView h_view, SlotButtonPresenter[] slots, ModuleSelectionPresenter moduleSelectionPresenter);
    CommandCenterPresenter CreateCommandCenterPresenter(ICommandCenterView cc_view, IModuleHandler m_handle, HullPanelPresenter hullPanelPresenter);

    ModuleSelectionPresenter CreateModuleSelectionPresenter(IModuleSelectionView sb_view, IModuleHandler m_handle, ModuleFactory moduleFactory);
    ResourceTradePresenter CreateResourceTradePresenter(
        IResourceTradeView resourceTradeView,
        IResourceManager resourceManager);

    public GameProgressPresenter CreateGameProgressPresenter(IGameProgressView gameProgressView, IGameProgress gameProgressModel);

    public ShipFunctionsPresenter CreateShipFunctionsPresenter(IShipFunctionsView s_view, IShipFunctionsModel shipFunctionsModel, IGameProgress gameProgress);

    public SlotButtonPresenter CreateSlotButtonPresenter(ISlotButtonView slotView, IModuleHandler moduleHandler, SlotMovementManager slotMovementManager, ModuleSelectionPresenter m_presenter);
 

}
