using System;

public class SlotButtonPresenter
{
    private readonly ISlotButtonView view;
    private readonly IModuleHandler moduleHandler;
    private readonly ModuleSelectionPresenter moduleSelectionPresenter;
    private readonly SlotMovementManager slotMovementManager;
    private Hull currentHull;
    private int slotIndex;

    public event Action<Hull> ModuleMoved;
    public event Action<IModule, Hull> ModuleUpgraded;

    public event Action<Hull, int> ModuleSold;
    public SlotButtonPresenter(ISlotButtonView view, IModuleHandler moduleHandler, SlotMovementManager slotMovementManager, ModuleSelectionPresenter m_presenter)
    {
        this.view = view;
        this.moduleHandler = moduleHandler;
        this.slotMovementManager = slotMovementManager;
        moduleSelectionPresenter = m_presenter;


        view.OnUpgradeButtonClicked += HandleUpgradeButtonClicked;
        view.OnSellButtonClicked += HandleSellButtonClicked;
        view.OnSlotButtonClicked += HandleSlotButtonClicked;
        view.OnRightArrowClicked += HandleRightArrowClicked;
        view.OnLeftArrowClicked += HandleLeftArrowClicked;
     
    }

    public void SetupSlot(Hull hull, int index)
    {
        currentHull = hull;
        slotIndex = index;

        var slot = hull.slots[slotIndex];
        if (slot.IsOccupied)
        {
            var module = slot.occupiedModule;
            view.SetSlotInfo(module.ModuleName, module.Level);
            if (module.Level < module.MaxLevel)
            {
                view.SetUpgradeButtonState(true, module.LevelPrices[module.Level]);
            }
            else
            {
                view.SetUpgradeButtonState(false, 0);
            }
            view.SetSellButtonState(true, module.LevelPrices[module.Level - 1] / 2);
        }
        else
        {
            view.SetSlotInfo("Empty", 0);
            view.SetUpgradeButtonState(false, 0);
            view.SetSellButtonState(false, 0);
        }
        view.SetupArrowButtons(hull, index);

    }

    private void HandleUpgradeButtonClicked()
    {
        var module = currentHull.slots[slotIndex].occupiedModule;
        if (moduleHandler.HandleModuleUpgrade(module))
        {
            SetupSlot(currentHull, slotIndex);
        }
    }

    private void HandleSellButtonClicked()
    {
        var module = currentHull.slots[slotIndex].occupiedModule;
        if (moduleHandler.HandleModuleSold(module, currentHull, slotIndex))
        {
            SetupSlot(currentHull, slotIndex); // Обновляем слот после продажи
            ModuleSold.Invoke(currentHull, slotIndex);
        }
    }
    private void HandleSlotButtonClicked()
    {
        var slot = currentHull.slots[slotIndex];
        if (!slot.IsOccupied)
        {
            moduleSelectionPresenter.OpenModuleSelection(currentHull, slotIndex);
     
        }
    }
    private void HandleLeftArrowClicked()
    {
        slotMovementManager.MoveModule(currentHull, slotIndex, slotIndex - 1);
        SetupSlot(currentHull, slotIndex - 1);
        ModuleMoved?.Invoke(currentHull);
    }

    private void HandleRightArrowClicked()
    {
        slotMovementManager.MoveModule(currentHull, slotIndex, slotIndex + 1);
        SetupSlot(currentHull, slotIndex + 1);
        ModuleMoved?.Invoke(currentHull);
    }
}